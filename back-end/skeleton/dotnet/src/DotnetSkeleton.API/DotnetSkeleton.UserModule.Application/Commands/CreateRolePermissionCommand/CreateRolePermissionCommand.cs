using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;

namespace DotnetSkeleton.UserModule.Application.Commands.CreateRolePermissionCommand
{
    public class CreateRolePermissionCommand : IRequest<BaseResponse>
    {
        public required int RoleId { get; set; }
        public required int PermissionId { get; set; }
    }
}
