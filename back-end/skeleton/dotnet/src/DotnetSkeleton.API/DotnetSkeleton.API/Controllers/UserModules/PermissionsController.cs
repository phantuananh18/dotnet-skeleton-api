using DotnetSkeleton.Core.Domain.Interfaces.Services;
using DotnetSkeleton.Core.Domain.Models.Requests.Permissions;

namespace DotnetSkeleton.API.Controllers.UserModules
{
    /// <summary>
    /// Permission Controller
    /// </summary>
    [ApiController]
    [ApiVersion("1")]
    [Extensions.Authorization.Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PermissionsController : ControllerBase
    {
        #region Private Fields
        private readonly IPermissionService _permissionService;

        #endregion

        #region Constructor
        public PermissionsController(IPermissionService permissionService)
        {
            _permissionService = permissionService ?? throw new ArgumentNullException(nameof(permissionService));
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
            var result = await _permissionService.GetPermissionByPermissionIdAsync(permissionId);
            return StatusCode(result.Status, result);
        }

        /// <summary>
        /// Get all permission with sort, pagination.
        /// </summary>
        /// <param name="request">Request parameters for pagination, sorting permissions.</param>
        /// <returns>List of permissions with the specified sorting and pagination applied.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllPermissionsAsync([FromQuery] GetAllPermissionsRequest request)
        {
            var result = await _permissionService.GetAllPermissionsAsync(request);
            return StatusCode(result.Status, result);
        }

        #endregion

        #region POST Methods
        // TO-DO: Implement POST methods

        /// <summary>
        /// Create a new permission with the provided information.
        /// </summary>
        /// <param name="request">The details of the permission to create.</param>
        /// <returns>An action result representing the result of the permission created process.</returns>
        [HttpPost]
        public async Task<IActionResult> CreatePermissionAsync([FromBody] CreatePermissionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
                
            var result = await _permissionService.CreatePermissionAsync(request);
            return StatusCode(result.Status, result);
        }

        #endregion

        #region PUT Methods
        // TO-DO: Implement PUT methods

        /// <summary>
        /// Update an permission with the provided information.
        /// </summary>
        /// <param name="request"><see cref="UpdatePermissionRequest"/> The details of the permission to update.</param>
        /// <returns>An action result representing the result of the permission updated process.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdatePermissionAsync(UpdatePermissionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _permissionService.UpdatedPermissionAsync(request);
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
            var result = await _permissionService.DeletePermissionAsync(permissionId);
            return StatusCode(result.Status, result);
        }

        #endregion

        #region PATCH Methods
        // TO-DO: Implement PATCH methods

        #endregion
    }
}
