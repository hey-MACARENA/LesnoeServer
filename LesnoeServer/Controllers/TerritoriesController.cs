using LesnoeServer.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LesnoeServer.Controllers
{
    [ApiController]
    [Route("api/territories")]
    public class TerritoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TerritoriesController(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: apt/teams
        [HttpGet]
        public async Task<List<Territories>> GetTerritories()
        {
            var territories = await _context.Territories.ToListAsync();

            return territories;
        }
    }
}
