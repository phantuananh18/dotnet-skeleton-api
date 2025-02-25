using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;

namespace DotnetSkeleton.UserModule.Application.Queries.GetByIdUserQuery
{
    public class GetByIdUserQuery : IRequest<BaseResponse>
    {
        public int UserId { get; set; }
    }
}
