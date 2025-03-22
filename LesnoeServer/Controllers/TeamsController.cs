using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LesnoeServer.Tables;
using Microsoft.Data.SqlClient;

namespace LesnoeServer.Controllers
{
    [ApiController]
    [Route("api/teams")]
    public class TeamsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TeamsController(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: apt/teams
        [HttpGet]
        public async Task<List<Teams>> GetTeamsById(int id)
        {
            var teams = await _context.Teams.ToListAsync();

            return teams;
        }
    }
}
