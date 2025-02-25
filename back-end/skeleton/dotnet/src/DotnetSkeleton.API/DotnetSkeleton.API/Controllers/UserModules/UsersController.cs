using DotnetSkeleton.Core.Domain.Interfaces.Services;
using DotnetSkeleton.Core.Domain.Models.Requests.Users;

namespace DotnetSkeleton.API.Controllers.UserModules;

/// <summary>
/// Users Controller
/// </summary>
[ApiController]
[ApiVersion("1")]
[Extensions.Authorization.Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
public class UsersController : ControllerBase
{
    #region Private Fields
    private readonly IUserService _userService;

    #endregion

    #region Constructor
    public UsersController(IUserService userService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
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
        var result = await _userService.GetUserByUserIdAsync(userId);
        return StatusCode(result.Status, result);
    }

    /// <summary>
    /// Get all users with filter, sort, pagination.
    /// </summary>
    /// <param name="request">The request containing filtering, sorting, and pagination information for retrieving users.</param>">
    /// <returns>List of users</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllUsersAsync([FromQuery] GetAllUsersRequest request)
    {
        var result = await _userService.GetAllUsersWithPaginationAsync(request);
        return StatusCode(result.Status, result);
    }

    #endregion

    #region POST Methods
    // TO-DO: Implement POST methods

    /// <summary>
    /// Create a new user with the provided information.
    /// </summary>
    /// <param name="request">The details of the user to create.</param>
    /// <returns>An action result representing the result of the user created process.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _userService.CreateUserAsync(request);
        return StatusCode(result.Status, result);
    }

    #endregion

    #region PUT Methods
    // TO-DO: Implement PUT methods

    /// <summary>
    /// Update an user with the provided information.
    /// </summary>
    /// <param name="userId">An id of user</param>
    /// <param name="request"><see cref="UpdateUserRequest"/> The details of the user to update.</param>
    /// <returns>An action result representing the result of the user updated process.</returns>
    [HttpPut]
    [Route("{userId}")]
    public async Task<IActionResult> UpdateUserAsync(int userId, UpdateUserRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _userService.UpdateUserAsync(userId, request);
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
        var result = await _userService.DeleteUserAsync(userId);
        return StatusCode(result.Status, result);
    }

    #endregion

    #region PATCH Methods
    // TO-DO: Implement PATCH methods

    #endregion
}