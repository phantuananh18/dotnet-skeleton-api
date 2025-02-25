using DotnetSkeleton.SharedKernel.Utils.Models.Requests;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;

namespace DotnetSkeleton.UserModule.Application.Queries.GetAllUsersQuery
{
    public class GetAllUsersQuery : PaginationBaseRequest, IRequest<BaseResponse>
    {

    }
}
