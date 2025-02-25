using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;

namespace DotnetSkeleton.UserModule.Application.Commands.DeleteRoleCommand
{
    public class DeleteRoleCommand : IRequest<BaseResponse>
    {
        public int RoleId { get; set; }
    }
}
