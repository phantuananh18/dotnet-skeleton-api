using Asp.Versioning;
using DotnetSkeleton.UserModule.Application.Commands.CreateUserCommand;
using DotnetSkeleton.UserModule.Application.Commands.DeleteUserCommand;
using DotnetSkeleton.UserModule.Application.Commands.UpdateUserCommand;
using DotnetSkeleton.UserModule.Application.Queries.GetAllUsersQuery;
using DotnetSkeleton.UserModule.Application.Queries.GetByIdUserQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DotnetSkeleton.UserModule.API.Controllers;

/// <summary>
/// Users Controller
/// </summary>
[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class UsersController : ControllerBase
{
    #region Private Fields
    private readonly IMediator _mediator;

    #endregion

    #region Constructor
    public UsersController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    #endregion

    #region GET Methods
    // TO-DO: Implement GET methods

    /// <summary>
    /// Get a user by userId
    /// </summary>
    /// <param name="userId">An id of user</param>
    /// <returns>User's detail information</returns>
    [HttpGet]
    [Route("{userId}")]
    public async Task<IActionResult> GetUserByUserIdAsync(int userId)
    {
        var result = await _mediator.Send(new GetByIdUserQuery() { UserId = userId });
        return StatusCode(result.Status, result);
    }

    /// <summary>
    /// Get all users with filter, sort, pagination.
    /// </summary>
    /// <returns>List of users</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllUsersAsync([FromQuery] GetAllUsersQuery query)
    {
        var result = await _mediator.Send(query);
        return StatusCode(result.Status, result);
    }

    #endregion

    #region POST Methods
    // TO-DO: Implement POST methods

    /// <summary>
    /// Create a new user with the provided information.
    /// </summary>
    /// <param name="command">The details of the user to create.</param>
    /// <returns>An action result representing the result of the user created process.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserCommand command)
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
    /// Update an user with the provided information.
    /// </summary>
    /// <param name="userId">An id of user</param>
    /// <param name="command"><see cref="UpdateUserCommand"/> The details of the user to update.</param>
    /// <returns>An action result representing the result of the user updated process.</returns>
    [HttpPut]
    [Route("{userId}")]
    public async Task<IActionResult> UpdateUserAsync(int userId, UpdateUserCommand command)
    {
        if (command == null || !ModelState.IsValid)
        {
            return BadRequest();
        }

        command.UserId = userId;
        var result = await _mediator.Send(command);
        return StatusCode(result.Status, result);
    }

    #endregion

    #region DELETE Methods
    // TO-DO: Implement DELETE methods

    /// <summary>
    /// Delete an existing user (soft delete)
    /// </summary>
    /// <param name="userId">An id of user</param>
    /// <returns>An action result representing the result of the user deleted process.</returns>
    [HttpDelete]
    [Route("{userId}")]
    public async Task<IActionResult> DeleteUserAsync(int userId)
    {
        var result = await _mediator.Send(new DeleteUserCommand { UserId = userId });
        return StatusCode(result.Status, result);
    }

    #endregion

    #region PATCH Methods
    // TO-DO: Implement PATCH methods

    #endregion
}