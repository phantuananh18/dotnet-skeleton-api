using DotnetSkeleton.EmailModule.Domain.Models.Requests;
using DotnetSkeleton.EmailModule.EmailWorker.Services.Interfaces;
using DotnetSkeleton.SharedKernel.Utils;
using DotnetSkeleton.SharedKernel.Utils.Models.Options;
using DotnetSkeleton.Utils.RabbitMqService.Interfaces;
using Microsoft.Extensions.Options;

namespace DotnetSkeleton.EmailModule.EmailWorker.Handler;

public class OutgoingEmailWorker : BackgroundService
{
    #region Private members
    private readonly ILogger<OutgoingEmailWorker> _logger;
    private readonly IRabbitMqService<OutgoingEmailRequest> _rabbitMqService;

    private readonly IEmailQueueService _emailQueueService;
    private readonly RabbitMqOptions _rabbitMqOptions;

    #endregion

    #region Constructor
    public OutgoingEmailWorker(ILogger<OutgoingEmailWorker> logger, IRabbitMqService<OutgoingEmailRequest> rabbitMqService,
        IOptionsMonitor<RabbitMqOptions> rabbitMqOptions, IEmailQueueService emailQueueService)
    {
        _logger = logger;
        _rabbitMqService = rabbitMqService;
        _emailQueueService = emailQueueService;
        _rabbitMqOptions = rabbitMqOptions.CurrentValue;
    }
    #endregion

    #region Worker
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _rabbitMqService.InitializeQueue(_rabbitMqOptions.EmailQueue.OutgoingEmailQueue);
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation("[OutgoingEmailWorker] Polling for new email at: {time}", DateTimeOffset.Now);

                // Pull message from queue and handle it with function QueueOutgoingEmailHandlerAsync
                await _rabbitMqService.PullAndHandleMessageAsync(_rabbitMqOptions.EmailQueue.OutgoingEmailQueue, 10,
                    _emailQueueService.OutgoingEmailHandlerAsync);

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError("[OutgoingEmailWorker] An error occurred while polling for emails. Exception: {message}",
                    Helpers.BuildErrorMessage(ex));
            }
        }

        _logger.LogInformation("[OutgoingEmailWorker] Worker stopped at: {time}", DateTimeOffset.Now);
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("[OutgoingEmailWorker] Worker is stopping.");
        await base.StopAsync(stoppingToken);
    }

    public override void Dispose()
    {
        base.Dispose();
    }

    #endregion
}