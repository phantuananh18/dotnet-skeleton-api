using DotnetSkeleton.SharedKernel.Utils.Models.Options;
using DotnetSkeleton.Utils.RabbitMqService.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using DotnetSkeleton.SharedKernel.Utils;

namespace DotnetSkeleton.Utils.RabbitMqService.Services;

public class RabbitMqService<TMessage> : IRabbitMqService<TMessage>, IDisposable where TMessage : class
{
    #region Private members
    private readonly ILogger<RabbitMqService<TMessage>> _logger;
    private readonly RabbitMqOptions _rabbitMqOptions;
    private IConnection? _connection;
    private IModel? _channel;

    #endregion

    #region Constructor
    public RabbitMqService(ILogger<RabbitMqService<TMessage>> logger, IOptionsMonitor<RabbitMqOptions> rabbitMqOptions)
    {
        _logger = logger;
        _rabbitMqOptions = rabbitMqOptions.CurrentValue;
    }

    #endregion

    #region Public methods
    /// <summary>
    /// Initializes the RabbitMQ queue with the specified name.
    /// </summary>
    /// <param name="queueName">The name of the queue to initialize.</param>
    public void InitializeQueue(string queueName)
    {
        var exchangeName = queueName.Contains("incoming")
            ? _rabbitMqOptions.EmailQueue.IncomingEmailExchange
            : _rabbitMqOptions.EmailQueue.OutgoingEmailExchange;
        var factory = new ConnectionFactory()
        {
            HostName = _rabbitMqOptions.HostName,
            Port = _rabbitMqOptions.Port,
            UserName = _rabbitMqOptions.Username,
            Password = _rabbitMqOptions.Password,
            VirtualHost = _rabbitMqOptions.VirtualHost,
            DispatchConsumersAsync = true
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        // Declare queue
        _channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
        _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        _channel.QueueBind(queueName, exchangeName, queueName, null);
        _logger.LogInformation("[RabbitMQService] Connected to queue {queue}", queueName);

        // Declare dead-letter queue
        _channel.ExchangeDeclare(_rabbitMqOptions.DeadLetterExchange, ExchangeType.Direct);
        _channel.QueueDeclare(_rabbitMqOptions.DeadLetterQueue, false, false, false, null);
        _channel.QueueBind(_rabbitMqOptions.DeadLetterQueue, _rabbitMqOptions.DeadLetterExchange, _rabbitMqOptions.DeadLetterQueue);
    }

    /// <summary>
    /// Sends a message to the specified RabbitMQ queue.
    /// </summary>
    /// <param name="queueName">The name of the queue where the message will be sent.</param>
    /// <param name="message">The message to be sent.</param>
    /// <param name="routingKey">Optional routing key to specify message routing. Default is an empty string.</param>
    /// <param name="properties">Optional RabbitMQ basic properties for the message. Default is null.</param>
    public void SendMessage(string queueName, TMessage message, string routingKey = "", IBasicProperties? properties = null)
    {
        EnsureChannelInitialized(queueName);
        var messageStr = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(messageStr);
        properties ??= AddDefaultProperties();

        _channel.BasicPublish(exchange: string.Empty, routingKey: routingKey, basicProperties: properties, body: body);
        _logger.LogInformation("[RabbitMQService] Sent a new message to RabbitMQ: {message}", messageStr);
    }

    /// <summary>
    /// Asynchronously pulls a message from the specified RabbitMQ queue and handles it using the provided function.
    /// </summary>
    /// <param name="queueName">The name of the queue to pull the message from.</param>
    /// <param name="messageCount">The number of messages to process.</param>
    /// <param name="messageHandler">A function that handles the message and returns a boolean indicating success.</param>
    public async Task PullAndHandleMessageAsync(string queueName, int messageCount, Func<TMessage, Task<bool>> messageHandler)
    {
        EnsureChannelInitialized(queueName);

        var countdownEvent = new CountdownEvent(messageCount);
        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.Received += async (ch, ea) =>
        {
            try
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                if (string.IsNullOrEmpty(message))
                {
                    _logger.LogWarning("[RabbitMQService] Received an empty message.");
                    _channel!.BasicAck(ea.DeliveryTag, false);
                    return;
                }

                _logger.LogInformation("[RabbitMQService] Receive a new message: {message}", message);

                // Handle max retry. If max retries reached, send to dead-letter queue
                var retryCount = GetRetryCount(ea.BasicProperties);
                if (retryCount >= _rabbitMqOptions.MaxRetryCount)
                {
                    _logger.LogError("[RabbitMQService] Max retries reached. Sending message to dead-letter queue.");
                    _channel!.BasicAck(ea.DeliveryTag, false);
                    SendDeadLetterQueue(ea, message);
                    return;
                }

                // Try parse message to TMessage's type. If failed, reject and requeue the message
                if (!TryParseMessage(message, out var eventMessage))
                {
                    _logger.LogWarning("[RabbitMQService] Message handling failed. Retrying (attempt {retryCount})", retryCount + 1);
                    HandleRetry(ea, retryCount);
                    return;
                }

                var processingResult = await messageHandler(eventMessage!);
                if (processingResult)
                {
                    // Acknowledge message
                    _channel!.BasicAck(ea.DeliveryTag, false);
                }
                else
                {
                    HandleRetry(ea, retryCount);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("[RabbitMQService] Exception occurred while handling the message. Ex: {ex}", ex.Message);
            }
            finally
            {
                countdownEvent.Signal();
                await Task.Yield();
            }
        };

        // Start consuming the queue with auto-acknowledgement set to false
        _channel.BasicConsume(queueName, false, consumer);
        await Task.Run(() => countdownEvent.Wait());

        _logger.LogInformation("[RabbitMQService] All messages have been processed.");
    }

    /// <summary>
    /// Deletes the specified RabbitMQ queue.
    /// </summary>
    /// <param name="queueName">The name of the queue to delete.</param>
    /// <param name="ifUnused">Deletes the queue only if it is unused.</param>
    /// <param name="ifEmpty">Deletes the queue only if it is empty.</param>
    public void DeleteQueue(string queueName, bool ifUnused, bool ifEmpty)
    {
        EnsureChannelInitialized(queueName);
        _channel!.QueueDelete(queueName, ifUnused, ifEmpty);
    }

    /// <summary>
    /// Purges all messages from the specified RabbitMQ queue.
    /// </summary>
    /// <param name="queueName">The name of the queue to purge.</param>
    public void PurgeQueue(string queueName)
    {
        EnsureChannelInitialized(queueName);
        _channel!.QueuePurge(queueName);
    }

    /// <summary>
    /// Acknowledges the message in the specified RabbitMQ queue with the given delivery tag.
    /// </summary>
    /// <param name="queueName">The name of the queue where the message was received.</param>
    /// <param name="deliveryTag">The delivery tag of the message to acknowledge.</param>
    /// <param name="isMultiple">Indicates whether to acknowledge multiple messages up to the delivery tag.</param>
    public void AcknowledgeMessage(string queueName, ulong deliveryTag, bool isMultiple)
    {
        EnsureChannelInitialized(queueName);
        _channel!.BasicAck(deliveryTag, isMultiple);
    }

    /// <summary>
    /// Rejects a message in the specified RabbitMQ queue with the given delivery tag and optionally re-queue it.
    /// </summary>
    /// <param name="queueName">The name of the queue where the message was received.</param>
    /// <param name="deliveryTag">The delivery tag of the message to reject.</param>
    /// <param name="requeue">Indicates whether the message should be re-queued.</param>
    public void Reject(string queueName, ulong deliveryTag, bool requeue)
    {
        EnsureChannelInitialized(queueName);
        _channel!.BasicReject(deliveryTag, requeue);
    }

    /// <summary>
    /// Disposes the RabbitMQ connection and channel, ensuring that they are properly closed and resources are released.
    /// </summary>
    public void Dispose()
    {
        if (_channel is { IsClosed: false })
        {
            _channel.Close();
        }
        if (_connection is { IsOpen: true })
        {
            _connection.Close();
        }

        _channel?.Dispose();
        _connection?.Dispose();
    }

    #endregion

    #region Private methods
    /// <summary>
    /// Ensures that the RabbitMQ channel is initialized and open. 
    /// If the channel is null or closed, it initializes the channel with the specified queue.
    /// </summary>
    /// <param name="queueName">The name of the queue to initialize if the channel is not ready.</param>
    private void EnsureChannelInitialized(string queueName)
    {
        if (_channel is null || _channel.IsClosed)
        {
            InitializeQueue(queueName);
        }
    }

    private IBasicProperties? AddDefaultProperties()
    {
        var properties = _channel!.CreateBasicProperties();
        properties.ContentType = "text/plain";
        properties.DeliveryMode = 2;
        properties.Headers = new Dictionary<string, object>();
        properties.Headers[Constant.HeaderAttribute.DeliveryCount] = 0;

        return properties;
    }

    private int GetRetryCount(IBasicProperties properties)
    {
        return properties.Headers is not null && properties.Headers.Count > 0
                                              && properties.Headers.TryGetValue(Constant.HeaderAttribute.DeliveryCount, out var retryValue)
            ? Convert.ToInt32(retryValue)
            : 0;
    }

    private void AddRetryCount(IBasicProperties properties)
    {
        if (properties.Headers is null)
        {
            properties.Headers = new Dictionary<string, object>();
            properties.Headers[Constant.HeaderAttribute.DeliveryCount] = 1;
        }

        if (!properties.Headers.TryGetValue(Constant.HeaderAttribute.DeliveryCount, out var retryValue))
        {
            return;
        }

        if (retryValue is null)
        {
            properties.Headers[Constant.HeaderAttribute.DeliveryCount] = 1;
        }
        else
        {
            var retryCount = Convert.ToInt32(retryValue);
            properties.Headers[Constant.HeaderAttribute.DeliveryCount] = retryCount + 1;
        }
    }

    private void SendDeadLetterQueue(BasicDeliverEventArgs ea, string message)
    {
        var deadLetterProperties = _channel!.CreateBasicProperties();
        deadLetterProperties.Persistent = true;
        deadLetterProperties.Headers = ea.BasicProperties.Headers;

        // Send the message to the dead-letter queue
        _channel.BasicPublish(exchange: _rabbitMqOptions.DeadLetterExchange,
            routingKey: _rabbitMqOptions.DeadLetterQueue,
            basicProperties: deadLetterProperties,
            body: ea.Body);

        _logger.LogInformation("[RabbitMQService] Message sent to dead-letter queue: {message}", message);
    }

    private bool TryParseMessage(string message, out TMessage? messageObj)
    {
        try
        {
            messageObj = JsonSerializer.Deserialize<TMessage>(message);
            if (messageObj is not null)
            {
                return true;
            }

            _logger.LogWarning("[RabbitMQService] Deserialization resulted in a null object for message: {message}", message);
            return false;
        }
        catch (JsonException ex)
        {
            _logger.LogError("[RabbitMQService] Exception occurred while deserializing message: {message}. Exception: {exception}", message, ex);
            messageObj = default;
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError("[RabbitMQService] Unexpected error while deserializing message: {message}. Exception: {exception}", message, ex);
            messageObj = default;
            return false;
        }
    }

    private void HandleRetry(BasicDeliverEventArgs ea, int retryCount)
    {
        // If the message handler return false, reject and requeue the message
        if (retryCount < _rabbitMqOptions.MaxRetryCount)
        {
            _logger.LogWarning("[RabbitMQService] Message handling failed. Retrying (attempt {retryCount})", retryCount + 1);
            AddRetryCount(ea.BasicProperties);
            _channel!.BasicPublish(ea.Exchange, ea.RoutingKey, ea.BasicProperties, ea.Body);
            _channel!.BasicReject(ea.DeliveryTag, false);
        }
        else
        {
            // Max retries reached, send to dead-letter queue
            _logger.LogError("[RabbitMQService] Max retries reached. Sending message to dead-letter queue.");
            _channel!.BasicAck(ea.DeliveryTag, false);
            SendDeadLetterQueue(ea, Encoding.UTF8.GetString(ea.Body.ToArray()));
        }
    }


    #endregion
}