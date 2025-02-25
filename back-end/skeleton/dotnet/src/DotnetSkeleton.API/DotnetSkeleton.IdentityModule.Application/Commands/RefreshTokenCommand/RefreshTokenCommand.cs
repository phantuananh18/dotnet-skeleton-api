using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;

namespace DotnetSkeleton.IdentityModule.Application.Commands.RefreshTokenCommand
{
    public class RefreshTokenCommand : IRequest<BaseResponse>
    {
        public required string RefreshToken { get; set; }
    }
}