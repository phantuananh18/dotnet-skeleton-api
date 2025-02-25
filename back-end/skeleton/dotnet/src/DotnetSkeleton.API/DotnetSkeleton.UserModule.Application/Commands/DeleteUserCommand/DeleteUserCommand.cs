using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;

namespace DotnetSkeleton.UserModule.Application.Commands.DeleteUserCommand
{
    public class DeleteUserCommand : IRequest<BaseResponse>
    {
        public int UserId { get; set; }
    }
}
