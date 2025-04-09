using LesnoeServer.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LesnoeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FireHazardLevelsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FireHazardLevelsController(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public async Task<List<Fire_hazard_levels>> GetTerritories()
        {
            var fire_hazard_levels = await _context.Fire_hazard_levels.ToListAsync();

            return fire_hazard_levels;
        }
    }
}
