using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;

namespace DotnetSkeleton.UserModule.Application.Commands.UpdatePermissionCommand
{
    public class UpdatePermissionCommand : IRequest<BaseResponse>
    {
        public int PermissionId { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
        public required string Description { get; set; }
    }
}
