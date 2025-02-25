using DotnetSkeleton.SharedKernel.Utils.Models.Requests;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;

namespace DotnetSkeleton.UserModule.Application.Queries.GetAllRolesQuery
{
    public class GetAllRolesQuery : PaginationBaseRequest, IRequest<BaseResponse>
    {

    }
}
