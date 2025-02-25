using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;

namespace DotnetSkeleton.UserModule.Application.Commands.DeleteRolePermissionCommand
{
    public class DeleteRolePermissionCommand : IRequest<BaseResponse>
    {
        public int RolePermissionId { get; set; }
    }
}
