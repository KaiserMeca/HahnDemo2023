using Domain.InterfacesServices;
using Domain.Validations;
using Microsoft.AspNetCore.Mvc;
using Domain.Assets.Models;

namespace AppWebApi.Controllers
{
    /// <summary>
    /// Controller for managing assets
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetService _services;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssetsController"/> class
        /// </summary>
        /// <param name="services">The asset service</param>
        public AssetsController(IAssetService services)
        {
            _services = services;
        }

        /// <summary>
        /// Retrieves all assets
        /// </summary>
        /// <returns>A list of assets</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssetDTO>>> GetAll()
        {
            var assets = await _services.GetAllAsync();
            if (assets == null || !assets.Any())
            {
                return Ok(new { message = "Empty" });
            }
            return Ok(assets);
        }

        /// <summary>
        /// Adds a new asset.
        /// </summary>
        /// <param name="asset">The asset to add</param>
        /// <returns>An action result indicating the success of the operation</returns>
        [HttpPost]
        public async Task<IActionResult> AddAsset(AssetDTO asset)
        {
            if (!await _services.AddAsync(asset))
            {
                return BadRequest(new { message = "Validation Error", errors = AssetValidation.Errors });
            }
            else
            {
                var baseUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
                var url = $"{baseUrl}/api/Asset/{asset.Name}";
                return Created(url, new { message = "Asset Created" });
            }
        }

        /// <summary>
        /// Retrieves an asset by its ID
        /// </summary>
        /// <param name="id">The ID of the asset to retrieve</param>
        /// <returns>An action result containing the asset if found, or an error message if not found</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsset(Guid id)
        {
            var asset = await _services.GetForIdAsync(id); ;
            if (asset == null)
            {
                return BadRequest(new { message = "The asset does not exist" });
            }
            else
            {
                return Ok(asset);
            }
        }

        /// <summary>
        /// Updates an asset by its ID
        /// </summary>
        /// <param name="id">The ID of the asset to update</param>
        /// <param name="assetDTO">The updated asset data</param>
        /// <returns>An action result indicating the success of the operation</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] AssetDTO assetDTO)
        {
            if (await _services.UpdateAsync(id, assetDTO))
            {
                return Ok(new { message = "Asset updated" });
            }
            return BadRequest(new
            {
                message = "The asset could not be updated",
                errors = AssetValidation.Errors
            });
        }

        /// <summary>
        /// Deletes an asset by its ID
        /// </summary>
        /// <param name="id">The ID of the asset to delete</param>
        /// <returns>An action result indicating the success of the operation</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await _services.DeleteAsync(id))
            {
                return BadRequest(new { message = "The asset you are trying to delete was not found in the database" });
            }
            else
            {
                return Ok(new { message = "Delete Asset" });
            }
        }
    }
}
