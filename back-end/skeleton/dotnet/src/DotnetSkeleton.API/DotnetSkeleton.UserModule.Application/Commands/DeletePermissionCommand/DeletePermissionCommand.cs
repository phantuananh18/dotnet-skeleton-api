using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;

namespace DotnetSkeleton.UserModule.Application.Commands.DeletePermissionCommand
{
    public class DeletePermissionCommand : IRequest<BaseResponse>
    {
        public int PermissionId { get; set; }
    }
}
