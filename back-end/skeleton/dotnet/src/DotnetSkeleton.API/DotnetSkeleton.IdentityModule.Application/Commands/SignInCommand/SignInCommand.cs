using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;

namespace DotnetSkeleton.IdentityModule.Application.Commands.SignInCommand
{
    public class SignInCommand : IRequest<BaseResponse>
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}