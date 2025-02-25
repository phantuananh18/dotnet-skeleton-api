using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using DotnetSkeleton.UserModule.Domain.Model.Requests.Permissions;
using MediatR;

namespace DotnetSkeleton.UserModule.Application.Commands.AssignRolePermissionsCommand
{
    public class AssignRolePermissionsCommand : IRequest<BaseResponse>
    {
        public required int RoleId { get; set; }
        public required List<AssignPermissionRequest> Permissions { get; set; }
    }
}
