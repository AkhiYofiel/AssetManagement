using AssetManagementApi.Dtos.SoftwareLicenses;
using AssetManagementApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoftwareLicensesController : ControllerBase
    {
        private readonly ISoftwareLicenseService _licenseService;

        public SoftwareLicensesController(ISoftwareLicenseService licenseService)
        {
            _licenseService = licenseService;
        }

        /// <summary>
        /// Gets all software licenses
        /// </summary>
        /// <returns>List of software licenses</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSoftwareLicenses()
        {
            var licenses = await _licenseService.GetSoftwareLicensesAsync();
            return Ok(licenses);
        }

        /// <summary>
        /// Get a software license
        /// </summary>
        /// <param name="id">The software license id</param>
        /// <returns>software license</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSoftwareLicense(int id)
        {
            var license = await _licenseService.GetSoftwareLicenseAsync(id);
            return license is null ? NotFound() : Ok(license);
        }

        /// <summary>
        /// Creates a new software license.
        /// </summary>
        /// <param name="dto">The software license data to create</param>
        /// <returns>The created software license</returns>
        [Authorize(Roles = "Admin,IT")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateSoftwareLicense(SoftwareLicenseCreateUpdateDto dto)
        {
            var result = await _licenseService.CreateSoftwareLicenseAsync(dto);

            if (!result.Succeeded)
            {
                return result.IsNotFound ? NotFound() : BadRequest(result.Errors);
            }

            return CreatedAtAction(nameof(GetSoftwareLicense), new { id = result.Data!.Id }, result.Data);
        }

        /// <summary>
        /// Update a software license
        /// </summary>
        /// <param name="id">The software license id</param>
        /// <param name="dto">The software license data to update.</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,IT")]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSoftwareLicense(int id, SoftwareLicenseCreateUpdateDto dto)
        {
            var result = await _licenseService.UpdateSoftwareLicenseAsync(id, dto);

            if (result.Succeeded)
            {
                return NoContent();
            }

            return result.IsNotFound ? NotFound() : BadRequest(result.Errors);
        }

        /// <summary>
        /// Delete a software license.
        /// </summary>
        /// <param name="id">The software license id</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,IT")]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSoftwareLicense(int id)
        {
            var result = await _licenseService.DeleteSoftwareLicenseAsync(id);

            if (result.Succeeded)
            {
                return NoContent();
            }

            return result.IsNotFound ? NotFound() : BadRequest(result.Errors);
        }
    }
}