using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;

namespace DotnetSkeleton.IdentityModule.Application.Commands.ResetPasswordCommand;

public class ResetPasswordCommand : IRequest<BaseResponse>
{
    public required string Token { get; set; }

    public required string NewPassword { get; set; }
}