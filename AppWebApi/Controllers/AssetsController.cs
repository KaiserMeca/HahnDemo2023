using Domain.Repositoy;
using Domain.Security;
using Microsoft.AspNetCore.Mvc;

namespace AppWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly IAssetRepository _services;

        public AssetsController(IAssetRepository services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssetDTO>>> GetAll()
        {
            var Assets = await _services.GetAllAsync();
            if (Assets == null || !Assets.Any())
            {
                return BadRequest(new { message = "Empty" });
            }
            return Ok(Assets);
        }
        [HttpPost]
        public async Task<IActionResult> AddAsset(AssetDTO asset)
        {
            if (!await _services.AddAsync(asset))
            {
                return BadRequest(new { message = "Error" });
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
            var response = await _services.GetForIdAsync(id);
            return Ok(response);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] AssetDTO asset)
        {
            if (!await _services.UpdateAsync(id, asset))
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await _services.DeleteAsync(id))
            {
                return BadRequest(new { message = "Error" });
            }
            else
            {
                return Ok(new { message = "Delete Asset" });
            }
        }
    }

}
