using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LesnoeServer.Tables;

namespace LesnoeServer.Controllers
{
    [ApiController]
    [Route("api/positions")]
    public class PositionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PositionsController(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: apt/positions
        [HttpGet]
        public async Task<List<Positions>> GetTeamsById(int id)
        {
            var positions = await _context.Positions.ToListAsync();

            return positions;
        }
    }
}
