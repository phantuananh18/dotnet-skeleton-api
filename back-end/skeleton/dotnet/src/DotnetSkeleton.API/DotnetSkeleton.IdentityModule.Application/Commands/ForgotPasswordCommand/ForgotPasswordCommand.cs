using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;

namespace DotnetSkeleton.IdentityModule.Application.Commands.ForgotPasswordCommand
{
    public class ForgotPasswordCommand : IRequest<BaseResponse>
    {
        public required string Email { get; set; }
    }
}