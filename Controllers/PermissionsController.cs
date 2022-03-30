using Microsoft.AspNetCore.Mvc;
using testAutofac.Data.Entities;
using testAutofac.Data.Requests;
using testAutofac.Data.Services;
using testAutofac.Definitions;

namespace testAutofac.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : BaseController
    {
        private readonly IPermissionService PermissionService;

        public PermissionsController(IPermissionService permissionService)
        {
            PermissionService = permissionService;
        }

        /// <summary>
        /// Upload Permission
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePermissionRequest request)
        {
            (Permission? Permission, string? errorMessage) = await PermissionService.CreateAsync(request);
            return Ok(Permission, Messages.Permissions.CreateSuccessfully);
        }

        /// <summary>
        /// Update Permission
        /// </summary>
        [HttpPut("{PermissionId}")]
        public async Task<IActionResult> UpdateAsync(Guid PermissionId, [FromBody] UpdatePermissionRequest request)
        {
            Permission? permission = await PermissionService.GetByIdAsync(PermissionId);
            if (permission == null)
            {
                return BadRequest(Messages.Permissions.CodeNotFound);
            }
            (Permission? newPermission, string? errorMessage) = await PermissionService.UpdateAsync(permission, request);
            return Ok(newPermission, Messages.Permissions.UpdatePermissionSuccessfully);
        }

        /// <summary>
        /// Delete Permission
        /// </summary>
        [HttpDelete("{PermissionId}")]
        public async Task<IActionResult> DeleteAsync(Guid PermissionId)
        {
            Permission? permission = await PermissionService.GetByIdAsync(PermissionId);
            if (permission == null)
            {
                return BadRequest(Messages.Permissions.CodeNotFound);
            }
            (Permission? newPermission, string? errorMessage) = await PermissionService.DeleteAsync(permission);
            return Ok(newPermission, Messages.Permissions.DeletePermissionSuccessfully);
        }
    }
}