using LesnoeServer.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace LesnoeServer.Controllers
{
    [ApiController]
    [Route("api/sectionsfire")]
    public class SectionsFireController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SectionsFireController(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: apt/sectionsfire
        [HttpGet]
        public async Task<List<SectionsFire>> GetSectionsFireAsync()
        {
            return await _context.Set<SectionsFire>()
                                 .FromSqlRaw("EXEC GetSectionsWithFireSafetyMeasures")
                                 .ToListAsync();
        }
    }
}
