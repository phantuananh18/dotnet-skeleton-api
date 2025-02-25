using DotnetSkeleton.SharedKernel.Utils.Models.Requests;

namespace DotnetSkeleton.Core.Domain.Models.Requests.Permissions
{
    public class GetAllPermissionsRequest : PaginationBaseRequest
    {
        public required string RoleName { get; set; }
    }
}