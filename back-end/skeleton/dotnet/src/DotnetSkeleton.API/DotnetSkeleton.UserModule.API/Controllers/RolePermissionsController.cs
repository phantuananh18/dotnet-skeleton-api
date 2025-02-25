using Asp.Versioning;
using DotnetSkeleton.UserModule.Application.Commands.CreateRolePermissionCommand;
using DotnetSkeleton.UserModule.Application.Commands.DeleteRolePermissionCommand;
using DotnetSkeleton.UserModule.Application.Commands.UpdateRolePermissionCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DotnetSkeleton.UserModule.API.Controllers;

/// <summary>
/// RolePermission Controller
/// </summary>
//[Extensions.Authorization.Authorize]
[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class RolePermissionsController : ControllerBase
{
    #region Private Fields
    private readonly IMediator _mediator;

    #endregion

    #region Constructor
    public RolePermissionsController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    #endregion

    #region POST Methods
    // TO-DO: Implement POST methods

    /// <summary>
    /// Create a new rolePermission with the provided information.
    /// </summary>
    /// <param name="command">The details of the rolePermission to create.</param>
    /// <returns>An action result representing the result of the rolePermission created process.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateRolePermissionAsync([FromBody] CreateRolePermissionCommand command)
    {
        if (!ModelState.IsValid)
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
    /// Update an rolePermission with the provided information.
    /// </summary>
    /// <param name="command"><see cref="UpdateRolePermissionCommand"/> The details of the rolePermission to update.</param>
    /// <returns>An action result representing the result of the rolePermission updated process.</returns>
    [HttpPut]
    public async Task<IActionResult> UpdateRolePermissionAsync(UpdateRolePermissionCommand command)
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
    /// Delete an existing rolePermission (soft delete)
    /// </summary>
    /// <param name="rolePermissionId">An id of rolePermission</param>
    /// <returns>An action result representing the result of the rolePermission deleted process.</returns>
    [HttpDelete("{rolePermissionId}")]
    public async Task<IActionResult> DeleteRolePermissionAsync(int rolePermissionId)
    {
        var result = await _mediator.Send(new DeleteRolePermissionCommand { RolePermissionId = rolePermissionId });
        return StatusCode(result.Status, result);
    }

    #endregion

    #region PATCH Methods
    // TO-DO: Implement PATCH methods

    #endregion
}