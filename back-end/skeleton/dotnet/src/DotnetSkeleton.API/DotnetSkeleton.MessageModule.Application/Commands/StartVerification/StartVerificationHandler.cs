using DotnetSkeleton.MessageModule.Domain.Interfaces.Services;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;

namespace DotnetSkeleton.MessageModule.Application.Commands.StartVerification
{
    public class StartVerificationHandler : IRequestHandler<StartVerificationCommand, BaseResponse>
    {
        private readonly IMessagingService _messagingService;

        public StartVerificationHandler(IMessagingService messagingService)
        {
            _messagingService = messagingService;
        }

        public async Task<BaseResponse> Handle(StartVerificationCommand command, CancellationToken cancellationToken)
        {
            try
            {
                return await _messagingService.StartVerificationAsync(command.MobilePhone, command.Chanel);
            }
            catch (Exception ex)
            {
                return BaseResponse.ServerError(ex.Message);
            }
        }
    }
}
