using AutoMapper;
using DotnetSkeleton.NotificationModule.Domain.Interfaces.Services;
using DotnetSkeleton.NotificationModule.Domain.Model.Requests.MessageInstance;
using DotnetSkeleton.SharedKernel.Utils;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DotnetSkeleton.NotificationModule.Application.Commands.NotificationMessageCommand
{
    public class SendNotificationHandler : IRequestHandler<SendNotificationCommand, BaseResponse>
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;
        private readonly ILogger<SendNotificationHandler> _logger;

        public SendNotificationHandler(INotificationService notificationService, IMapper mapper, ILogger<SendNotificationHandler> logger)
        {
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<BaseResponse> Handle(SendNotificationCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var request = _mapper.Map<SendNotificationRequest>(command);
                return await _notificationService.SendNotificationAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[SendNotificationHandler] - {Helpers.BuildErrorMessage(ex)}");
                return BaseResponse.ServerError();
            }
        }
    }
}