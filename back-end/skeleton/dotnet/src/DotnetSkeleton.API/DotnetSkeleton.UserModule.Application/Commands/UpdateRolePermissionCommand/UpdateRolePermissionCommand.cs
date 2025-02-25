using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;

namespace DotnetSkeleton.UserModule.Application.Commands.UpdateRolePermissionCommand
{
    public class UpdateRolePermissionCommand : IRequest<BaseResponse>
    {
        public required int RolePermissionId { get; set; }
        public required int RoleId { get; set; }
        public required int PermissionId { get; set; }
    }
}
