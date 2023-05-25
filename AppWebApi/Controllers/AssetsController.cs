using Domain.InterfacesServices;
using Domain.Assets;
using Domain.Validations;
using Microsoft.AspNetCore.Mvc;

namespace AppWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetService _services;

        public AssetsController(IAssetService services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssetDTO>>> GetAll()
        {
            var Assets = await _services.GetAllAsync();
            if (Assets == null || !Assets.Any())
            {
                return Ok(new { message = "Empty" });
            }
            return Ok(Assets);
        }
        [HttpPost]
        public async Task<IActionResult> AddAsset(AssetDTO asset)
        {
            if (!await _services.AddAsync(asset))
            {
                return BadRequest(new { message = "Validation Error", errors = AssetValidation.errors });
            }
            else
            {
                var baseUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
                var url = $"{baseUrl}/api/Asset/{asset.Name}";
                return Created(url, new { message = "Asset Created" });
            }
        }
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] AssetDTO asset)
        {
            if (!await _services.UpdateAsync(id, asset))
            {
                return BadRequest(new { message = "The asset could not be updated"
                , errors = AssetValidation.errors
                });
            }
            else
            {
                return Ok(new { message = "Asset updated" });
            }
        }

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
