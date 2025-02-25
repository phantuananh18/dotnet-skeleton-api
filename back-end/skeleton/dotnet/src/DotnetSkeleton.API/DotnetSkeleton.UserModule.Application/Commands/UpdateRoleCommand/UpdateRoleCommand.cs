using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;

namespace DotnetSkeleton.UserModule.Application.Commands.UpdateRoleCommand
{
    public class UpdateRoleCommand : IRequest<BaseResponse>
    {
        public int RoleId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}
