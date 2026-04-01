using AssetManagementApi.Dtos.Assets;
using AssetManagementApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetService _assetService;

        public AssetsController(IAssetService assetService)
        {
            _assetService = assetService;
        }

        /// <summary>
        /// Gets all assets
        /// </summary>
        /// <returns>List of assets</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAssets()
        {
            var assets = await _assetService.GetAssetsAsync();
            return Ok(assets);
        }

        /// <summary>
        /// Get an asset
        /// </summary>
        /// <param name="id">The asset id</param>
        /// <returns>The asset</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsset(int id)
        {
            var asset = await _assetService.GetAssetAsync(id);
            return asset is null ? NotFound() : Ok(asset);
        }

        /// <summary>
        /// Creates a new asset
        /// </summary>
        /// <param name="dto">The asset data to create</param>
        /// <returns>The created asset</returns>
        [Authorize(Roles = "Admin,IT")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsset(AssetCreateDto dto)
        {
            var result = await _assetService.CreateAssetAsync(dto);

            if (!result.Succeeded)
            {
                return result.IsNotFound ? NotFound() : BadRequest(result.Errors);
            }

            return CreatedAtAction(nameof(GetAsset), new { id = result.Data!.Id }, result.Data);
        }

        /// <summary>
        /// Updates an existing asset
        /// </summary>
        /// <param name="id">The asset id</param>
        /// <param name="dto">The asset data to update</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,IT")]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsset(int id, AssetUpdateDto dto)
        {
            var result = await _assetService.UpdateAssetAsync(id, dto);

            if (result.Succeeded)
            {
                return NoContent();
            }

            return result.IsNotFound ? NotFound() : BadRequest(result.Errors);
        }

        /// <summary>
        /// Deletes an asset
        /// </summary>
        /// <param name="id">The asset id</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,IT")]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsset(int id)
        {
            var result = await _assetService.DeleteAssetAsync(id);

            if (result.Succeeded)
            {
                return NoContent();
            }

            return result.IsNotFound ? NotFound() : BadRequest(result.Errors);
        }

        /// <summary>
        /// Assigns a software license to an asset.
        /// </summary>
        /// <param name="id">The asset identifier.</param>
        /// <param name="licenseId">The software license identifier.</param>
        /// <returns>No content.</returns>
        [Authorize(Roles = "Admin,IT")]
        [HttpPost("{id:int}/licenses/{licenseId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AssignLicense(int id, int licenseId)
        {
            var result = await _assetService.AssignLicenseAsync(id, licenseId);

            if (result.Succeeded)
            {
                return NoContent();
            }

            return result.IsNotFound ? NotFound() : BadRequest(result.Errors);
        }

        ///// <summary>
        ///// Removes a software license from an asset.
        ///// </summary>
        ///// <param name="id">The asset identifier.</param>
        ///// <param name="licenseId">The software license identifier.</param>
        ///// <returns>No content.</returns>
        //[Authorize(Roles = "Admin,IT")]
        //[HttpDelete("{id:int}/licenses/{licenseId:int}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> RemoveLicense(int id, int licenseId)
        //{
        //    var result = await _assetService.RemoveLicenseAsync(id, licenseId);
        //    return result.Status == OperationStatus.NotFound ? NotFound() : NoContent();
        //}
    }
}