using RabbitMQ.Client;

namespace DotnetSkeleton.Utils.RabbitMqService.Interfaces;

public interface IRabbitMqService<TMessage> where TMessage : class
{
    /// <summary>
    /// Initializes the RabbitMQ queue with the specified name.
    /// </summary>
    /// <param name="queueName">The name of the queue to initialize.</param>
    void InitializeQueue(string queueName);

    /// <summary>
    /// Sends a message to the specified RabbitMQ queue.
    /// </summary>
    /// <param name="queueName">The name of the queue where the message will be sent.</param>
    /// <param name="message">The message to be sent.</param>
    /// <param name="routingKey">Optional routing key to specify message routing. Default is an empty string.</param>
    /// <param name="properties">Optional RabbitMQ basic properties for the message. Default is null.</param>
    void SendMessage(string queueName, TMessage message, string routingKey = "", IBasicProperties? properties = null);

    /// <summary>
    /// Asynchronously pulls a message from the specified RabbitMQ queue and handles it using the provided function.
    /// </summary>
    /// <param name="queueName">The name of the queue to pull the message from.</param>
    /// <param name="messageCount">The number of messages to process.</param>
    /// <param name="messageHandler">A function that handles the message and returns a boolean indicating success.</param>
    Task PullAndHandleMessageAsync(string queueName, int messageCount, Func<TMessage, Task<bool>> messageHandler);

    /// <summary>
    /// Deletes the specified RabbitMQ queue.
    /// </summary>
    /// <param name="queueName">The name of the queue to delete.</param>
    /// <param name="ifUnused">Deletes the queue only if it is unused.</param>
    /// <param name="ifEmpty">Deletes the queue only if it is empty.</param>
    void DeleteQueue(string queueName, bool ifUnused, bool ifEmpty);

    /// <summary>
    /// Purges all messages from the specified RabbitMQ queue.
    /// </summary>
    /// <param name="queueName">The name of the queue to purge.</param>
    void PurgeQueue(string queueName);

    /// <summary>
    /// Acknowledges the message in the specified RabbitMQ queue with the given delivery tag.
    /// </summary>
    /// <param name="queueName">The name of the queue where the message was received.</param>
    /// <param name="deliveryTag">The delivery tag of the message to acknowledge.</param>
    /// <param name="isMultiple">Indicates whether to acknowledge multiple messages up to the delivery tag.</param>
    void AcknowledgeMessage(string queueName, ulong deliveryTag, bool isMultiple);

    /// <summary>
    /// Rejects a message in the specified RabbitMQ queue with the given delivery tag and optionally re-queues it.
    /// </summary>
    /// <param name="queueName">The name of the queue where the message was received.</param>
    /// <param name="deliveryTag">The delivery tag of the message to reject.</param>
    /// <param name="requeue">Indicates whether the message should be re-queued.</param>
    void Reject(string queueName, ulong deliveryTag, bool requeue);
}