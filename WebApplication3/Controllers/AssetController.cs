using DBProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DBProject.Controllers;

[ApiController]
[Route("[controller]")]
public class AssetController : ControllerBase
{
   private readonly DBProjectContext _context;

   public AssetController(DBProjectContext context)
   {
       _context = context;
   }

   [HttpGet]
   public async Task<ActionResult<IEnumerable<Asset>>> GetAssets()
   {
       return await _context.Assets.ToListAsync();
   }

   [HttpGet]
   [Route("{AssetID}")]
   public async Task<ActionResult<Asset>> GetAsset(int AssetID)
   {
       var asset = await _context.Assets.FindAsync(AssetID);
       if (asset == null) return NotFound(new { Message = "Asset not found." });
       return asset;
   }

   [HttpPost]
   public async Task<ActionResult<Asset>> PostAsset(Asset asset)
   {
       _context.Assets.Add(asset);
       await _context.SaveChangesAsync();
       return CreatedAtAction(nameof(GetAsset), new { AssetID = asset.AssetID }, asset);
   }


   [HttpDelete]
   [Route("{AssetID}")]
   public async Task<ActionResult<Asset>> DeleteAsset(int AssetID)
   {
       var asset = await _context.Assets.FindAsync(AssetID);
       if (asset == null) return NotFound(new { Message = "Asset not found." });
       _context.Assets.Remove(asset);
       await _context.SaveChangesAsync();
       return asset;
   }
}