using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using MediatR;

namespace DotnetSkeleton.UserModule.Application.Queries.GetByIdRoleQuery
{
    public class GetByIdRoleQuery : IRequest<BaseResponse>
    {
        public int RoleId { get; set; }
    }
}
