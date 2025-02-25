using DotnetSkeleton.Core.Domain.Interfaces.Services;
using DotnetSkeleton.Core.Domain.Models.Requests.RolePermissions;

namespace DotnetSkeleton.API.Controllers.UserModules
{
    /// <summary>
    /// RolePermission Controller
    /// </summary>
    [ApiController]
    [ApiVersion("1")]
    [Extensions.Authorization.Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RolePermissionsController : ControllerBase
    {
        #region Private Fields
        private readonly IRolePermissionService _rolePermissionService;

        #endregion

        #region Constructor
        public RolePermissionsController(IRolePermissionService rolePermissionService)
        {
            _rolePermissionService = rolePermissionService ?? throw new ArgumentNullException(nameof(rolePermissionService));
        }

        #endregion

        #region POST Methods
        // TO-DO: Implement POST methods

        /// <summary>
        /// Create a new rolePermission with the provided information.
        /// </summary>
        /// <param name="request">The details of the rolePermission to create.</param>
        /// <returns>An action result representing the result of the rolePermission created process.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateRolePermissionAsync([FromBody] CreateRolePermissionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _rolePermissionService.CreateRolePermissionAsync(request);
            return StatusCode(result.Status, result);
        }

        #endregion

        #region PUT Methods
        // TO-DO: Implement PUT methods

        /// <summary>
        /// Update an rolePermission with the provided information.
        /// </summary>
        /// <param name="request"><see cref="UpdateRolePermissionRequest"/> The details of the rolePermission to update.</param>
        /// <returns>An action result representing the result of the rolePermission updated process.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateRolePermissionAsync(UpdateRolePermissionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _rolePermissionService.UpdatedRolePermissionAsync(request);
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
            var result = await _rolePermissionService.DeleteRolePermissionAsync(rolePermissionId);
            return StatusCode(result.Status, result);
        }

        #endregion

        #region PATCH Methods
        // TO-DO: Implement PATCH methods

        #endregion
    }
}
