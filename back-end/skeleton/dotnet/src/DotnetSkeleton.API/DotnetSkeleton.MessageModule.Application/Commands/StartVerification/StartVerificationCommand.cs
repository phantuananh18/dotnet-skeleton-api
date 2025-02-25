using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;

namespace DotnetSkeleton.MessageModule.Application.Commands.StartVerification
{
    public class StartVerificationCommand : IRequest<BaseResponse>
    {
        public required string MobilePhone { get; set; }
        public required string Chanel { get; set; }
    }
}
