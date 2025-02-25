using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;

namespace DotnetSkeleton.UserModule.Application.Queries.GetByIdPermissionQuery
{
    public class GetByIdPermissionQuery : IRequest<BaseResponse>
    {
        public int PermissionId { get; set; }
    }
}
