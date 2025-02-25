using DotnetSkeleton.Core.Domain.Interfaces.Services;
using DotnetSkeleton.Core.Domain.Models.Requests.RolePermissions;
using DotnetSkeleton.Core.Domain.Models.Requests.Roles;

namespace DotnetSkeleton.API.Controllers.UserModules
{
    /// <summary>
    /// Role Controller
    /// </summary>
    [ApiController]
    [ApiVersion("1")]
    [Extensions.Authorization.Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RolesController : ControllerBase
    {
        #region Private Fields
        private readonly IRoleService _roleService;

        #endregion

        #region Constructor
        public RolesController(IRoleService roleService)
        {
            _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
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
            var result = await _roleService.GetRoleByRoleIdAsync(roleId);
            return StatusCode(result.Status, result);
        }

        /// <summary>
        /// Get all roles with search text, sort and pagination.
        /// </summary>
        /// <param name="request">The details of the role to create.</param>
        /// <returns>List of roles</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllRolesWithPaginationAsync([FromQuery] GetAllRolesRequest request)
        {
            var result = await _roleService.GetAllRolesWithPaginationAsync(request);
            return StatusCode(result.Status, result);
        }

        #endregion

        #region POST Methods
        // TO-DO: Implement POST methods

        /// <summary>
        /// Create a new role with the provided information.
        /// </summary>
        /// <param name="request">The details of the role to create.</param>
        /// <returns>An action result representing the result of the role created process.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateRoleAsync([FromBody] CreateRoleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _roleService.CreateRoleAsync(request);
            return StatusCode(result.Status, result);
        }

        /// <summary>
        /// Manager role permissions by RoleId 
        /// </summary>
        /// <param name="roleId">RoleId</param>
        /// <param name="request">The details of the role permissions to assign.</param>
        /// <returns>An action result representing the result of the role permissions assigned process.</returns>
        [HttpPost("{roleId}/assign-permission")]
        public async Task<IActionResult> AssignRolePermissionAsync([FromRoute] int roleId, [FromBody] AssignRolePermissionRequest request)
        {
            if (!ModelState.IsValid || roleId < 0)
            {
                return BadRequest();
            }

            var result = await _roleService.AssignRolePermissionsAsync(roleId, request);
            return StatusCode(result.Status, result);
        }

        #endregion

        #region PUT Methods
        // TO-DO: Implement PUT methods

        /// <summary>
        /// Update an role with the provided information.
        /// </summary>
        /// <param name="request"><see cref="UpdateRoleRequest"/> The details of the role to update.</param>
        /// <returns>An action result representing the result of the role updated process.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateRoleAsync(UpdateRoleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _roleService.UpdatedRoleAsync(request);
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
            var result = await _roleService.DeleteRoleAsync(roleId);
            return StatusCode(result.Status, result);
        }

        #endregion

        #region PATCH Methods
        // TO-DO: Implement PATCH methods

        #endregion
    }
}
