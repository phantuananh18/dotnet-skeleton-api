using DotnetSkeleton.MessageModule.Domain.Interfaces.Services;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;

namespace DotnetSkeleton.MessageModule.Application.Commands.CheckVerification
{
    public class CheckVerificationHandler : IRequestHandler<CheckVerificationCommand, BaseResponse>
    {
        private readonly IMessagingService _messagingService;

        public CheckVerificationHandler(IMessagingService messagingService)
        {
            _messagingService = messagingService;
        }

        public async Task<BaseResponse> Handle(CheckVerificationCommand command, CancellationToken cancellationToken)
        {
            try
            {
                return await _messagingService.CheckVerificationAsync(command.MobilePhone, command.Code);
            }
            catch (Exception ex)
            {
                return BaseResponse.ServerError(ex.Message);
            }
        }
    }
}
