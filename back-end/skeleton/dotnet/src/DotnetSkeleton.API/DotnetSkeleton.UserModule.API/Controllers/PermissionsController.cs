﻿using Asp.Versioning;
using DotnetSkeleton.UserModule.Application.Commands.CreatePermissionCommand;
using DotnetSkeleton.UserModule.Application.Commands.DeletePermissionCommand;
using DotnetSkeleton.UserModule.Application.Commands.UpdatePermissionCommand;
using DotnetSkeleton.UserModule.Application.Queries.GetAllPermissionsQuery;
using DotnetSkeleton.UserModule.Application.Queries.GetByIdPermissionQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DotnetSkeleton.UserModule.API.Controllers;

/// <summary>
/// Permission Controller
/// </summary>
[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class PermissionsController : ControllerBase
{
    #region Private Fields
    private readonly IMediator _mediator;

    #endregion

    #region Constructor
    public PermissionsController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    #endregion

    #region GET Methods
    // TO-DO: Implement GET methods

    /// <summary>
    /// Get a permission by permissionId
    /// </summary>
    /// <param name="permissionId">An id of permission</param>
    /// <returns> List of permissions </returns>
    [HttpGet("{permissionId}")]
    public async Task<IActionResult> GetPermissionByPermissionIdAsync(int permissionId)
    {
        var result = await _mediator.Send(new GetByIdPermissionQuery() { PermissionId = permissionId });
        return StatusCode(result.Status, result);
    }

    /// <summary>
    /// Get all permission with sort, pagination.
    /// </summary>
    /// <param name="query">Query parameters for pagination, sorting permissions.</param>
    /// <returns>List of permissions with the specified sorting and pagination applied.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllPermissionsAsync([FromQuery] GetAllPermissionsQuery query)
    {
        var result = await _mediator.Send(query);
        return StatusCode(result.Status, result);
    }

    #endregion

    #region POST Methods
    // TO-DO: Implement POST methods

    /// <summary>
    /// Create a new permission with the provided information.
    /// </summary>
    /// <param name="command">The details of the permission to create.</param>
    /// <returns>An action result representing the result of the permission created process.</returns>
    [HttpPost]
    public async Task<IActionResult> CreatePermissionAsync([FromBody] CreatePermissionCommand command)
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
    /// Update an permission with the provided information.
    /// </summary>
    /// <param name="command"><see cref="UpdatePermissionCommand"/> The details of the permission to update.</param>
    /// <returns>An action result representing the result of the permission updated process.</returns>
    [HttpPut]
    public async Task<IActionResult> UpdatePermissionAsync(UpdatePermissionCommand command)
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
    /// Delete an existing permission (soft delete)
    /// </summary>
    /// <param name="permissionId">An id of permission</param>
    /// <returns>An action result representing the result of the permission deleted process.</returns>
    [HttpDelete("{permissionId}")]
    public async Task<IActionResult> DeletePermissionAsync(int permissionId)
    {
        var result = await _mediator.Send(new DeletePermissionCommand { PermissionId = permissionId });
        return StatusCode(result.Status, result);
    }

    #endregion

    #region PATCH Methods
    // TO-DO: Implement PATCH methods

    #endregion
}