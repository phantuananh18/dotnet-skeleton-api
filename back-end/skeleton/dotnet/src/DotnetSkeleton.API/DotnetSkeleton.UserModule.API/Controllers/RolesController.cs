using Asp.Versioning;
using DotnetSkeleton.UserModule.Application.Commands.AssignRolePermissionsCommand;
using DotnetSkeleton.UserModule.Application.Commands.CreateRoleCommand;
using DotnetSkeleton.UserModule.Application.Commands.DeleteRoleCommand;
using DotnetSkeleton.UserModule.Application.Commands.UpdateRoleCommand;
using DotnetSkeleton.UserModule.Application.Queries.GetAllRolesQuery;
using DotnetSkeleton.UserModule.Application.Queries.GetByIdRoleQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DotnetSkeleton.UserModule.API.Controllers;

/// <summary>
/// Role Controller
/// </summary>
[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class RolesController : ControllerBase
{
    #region Private Fields
    private readonly IMediator _mediator;

    #endregion

    #region Constructor
    public RolesController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    #endregion

    #region GET Methods
    // TO-DO: Implement GET methods

    /// <summary>
    /// Get a role by roleId
    /// </summary>
    /// <param name="roleId">An id of role</param>
    /// <returns> List of roles </returns>
    [HttpGet("{roleId}")]
    public async Task<IActionResult> GetRoleByRoleIdAsync(int roleId)
    {
        var result = await _mediator.Send(new GetByIdRoleQuery() { RoleId = roleId });
        return StatusCode(result.Status, result);
    }

    /// <summary>
    /// Get all roles with search text, sort and pagination.
    /// </summary>
    /// <returns>List of roles</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllRolesWithPaginationAsync([FromQuery] GetAllRolesQuery query)
    {
        var result = await _mediator.Send(query);
        return StatusCode(result.Status, result);
    }

    #endregion

    #region POST Methods
    // TO-DO: Implement POST methods

    /// <summary>
    /// Create a new role with the provided information.
    /// </summary>
    /// <param name="command">The details of the role to create.</param>
    /// <returns>An action result representing the result of the role created process.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateRoleAsync([FromBody] CreateRoleCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(command);
        return StatusCode(result.Status, result);
    }

    /// <summary>
    /// Manager role permissions by RoleId 
    /// </summary>
    /// <param name="roleId">RoleId</param>
    /// <param name="command">The details of the role permissions to assign.</param>
    /// <returns>An action result representing the result of the role permissions assigned process.</returns>
    [HttpPost("{roleId}/assign-permission")]
    public async Task<IActionResult> AssignRolePermissionAsync([FromRoute] int roleId, [FromBody] AssignRolePermissionsCommand command)
    {
        if (!ModelState.IsValid || roleId < 0)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(command);
        return StatusCode(result.Status, result);
    }

    #endregion

    #region PUT Methods
    // TO-DO: Implement PUT methods

    /// <summary>
    /// Update an role with the provided information.
    /// </summary>
    /// <param name="command"><see cref="UpdateRoleCommand"/> The details of the role to update.</param>
    /// <returns>An action result representing the result of the role updated process.</returns>
    [HttpPut]
    public async Task<IActionResult> UpdateRoleAsync(UpdateRoleCommand command)
    {
        if (command == null || !ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(command);
        return StatusCode(result.Status, result);
    }

    #endregion

    #region DELETE Methods
    // TO-DO: Implement DELETE methods

    /// <summary>
    /// Delete an existing role (soft delete)
    /// </summary>
    /// <param name="roleId">An id of role</param>
    /// <returns>An action result representing the result of the role deleted process.</returns>
    [HttpDelete("{roleId}")]
    public async Task<IActionResult> DeleteRoleAsync(int roleId)
    {
        var result = await _mediator.Send(new DeleteRoleCommand { RoleId = roleId });
        return StatusCode(result.Status, result);
    }

    #endregion

    #region PATCH Methods
    // TO-DO: Implement PATCH methods

    #endregion
}