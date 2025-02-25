using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;

namespace DotnetSkeleton.MessageModule.Application.Commands.CheckVerification
{
    public class CheckVerificationCommand : IRequest<BaseResponse>
    {
        public required string MobilePhone { get; set; }
        public required string Code { get; set; }
    }
}
