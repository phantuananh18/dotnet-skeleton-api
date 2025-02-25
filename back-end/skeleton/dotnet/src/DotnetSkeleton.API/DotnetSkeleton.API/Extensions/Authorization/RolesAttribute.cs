namespace DotnetSkeleton.API.Extensions.Authorization;

/// <summary>
/// An attribute used to specify the roles allowed to access a controller or action method.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RolesAttribute : Attribute
{
    /// <summary>
    /// Gets the roles allowed to access the controller or action method.
    /// </summary>
    public string[] Roles { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RolesAttribute"/> class with the specified roles.
    /// </summary>
    /// <param name="roles">The roles allowed to access the controller or action method.</param>
    public RolesAttribute(params string[] roles)
    {
        Roles = roles;
    }
}