using DotnetSkeleton.SharedKernel.Utils;
using DotnetSkeleton.SharedKernel.Utils.Models;
using DotnetSkeleton.SharedKernel.Utils.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DotnetSkeleton.API.Extensions.Authorization;

/// <summary>
/// An attribute used to enforce authorization checks on controllers or action methods.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    /// <summary>
    /// Called when authorization is required for a controller or action method.
    /// </summary>
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
        {
            return;
        }

        var user = (UserProfileData)context.HttpContext.Items[Constant.FieldName.User]!;
        if (user is null)
        {
            context.Result = new ObjectResult(BaseResponse.Unauthorized());
            return;
        }

        var rolesAttribute = context.ActionDescriptor.EndpointMetadata.OfType<RolesAttribute>().FirstOrDefault();
        if (rolesAttribute == null)
            return;

        if (rolesAttribute.Roles.Contains(user.RoleName))
            return;

        context.Result = new ObjectResult(BaseResponse.Forbidden());
        return;
    }
}