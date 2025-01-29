using DBProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DBProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly DBProjectContext _context;

        public LocationController(DBProjectContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> Get()
        {
            return await _context.Locations.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> Get(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location == null) return NotFound(new { Message = "Location not found." });
            return location;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Location location)
        {
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = location.LocationID }, location);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location == null) return NotFound(new { Message = "Location not found." });

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Location deleted successfully." });
        }
    }
}