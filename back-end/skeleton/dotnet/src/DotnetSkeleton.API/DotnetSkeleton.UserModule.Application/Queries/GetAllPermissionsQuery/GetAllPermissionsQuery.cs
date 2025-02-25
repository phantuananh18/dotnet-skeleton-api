using DotnetSkeleton.SharedKernel.Utils.Models.Requests;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;

namespace DotnetSkeleton.UserModule.Application.Queries.GetAllPermissionsQuery
{
    public class GetAllPermissionsQuery : PaginationBaseRequest, IRequest<BaseResponse>
    {
        public required string RoleName { get; set; }
    }
}