using DotnetSkeleton.SharedKernel.Utils.Models.Requests;

namespace DotnetSkeleton.UserModule.Domain.Model.Requests.Permissions
{
    public class GetAllPermissionsRequest : PaginationBaseRequest
    {
        public required string RoleName { get; set; }
    }
}