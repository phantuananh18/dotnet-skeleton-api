using DotnetSkeleton.MessageModule.Domain.Interfaces.Services;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;

namespace DotnetSkeleton.MessageModule.Application.Commands.SendSms
{
    public class SendSmsHandler : IRequestHandler<SendSmsCommand, BaseResponse>
    {
        private readonly IMessagingService _messagingService;

        public SendSmsHandler(IMessagingService messagingService)
        {
            _messagingService = messagingService;
        }

        public async Task<BaseResponse> Handle(SendSmsCommand command, CancellationToken cancellationToken)
        {
            try
            {
                return await _messagingService.SendSmsAsync(command.ToMobilePhone, command.Message);
            }
            catch (Exception ex)
            {
                return BaseResponse.ServerError(ex.Message);
            }
        }
    }
}
