using LesnoeServer.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace LesnoeServer.Controllers
{
    [ApiController]
    [Route("api/sections")]
    public class SectionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SectionsController(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: apt/sections
        [HttpGet]
        public async Task<IActionResult> GetSectionsAsync(string? sort = null)
        {
            var sortParam = new SqlParameter("@sort", sort ?? (object)DBNull.Value);

            var sections = await _context.Set<SectionsDetails>()
                                  .FromSqlRaw("EXEC GetSectionsWithEmployees @sort", sortParam)
                                  .ToListAsync();

            var response = new
            {
                Data = sections,
                Count = sections.Count
            };

            return Ok(response);
        }

        // POST: api/sections
        [HttpPost]
        public async Task<ActionResult<Sections>> PostSection([FromBody] Sections section)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _context.Sections.Add(section);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetSectionById),
                    new { id = section.section_id },
                    section);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/sections/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSection(
            [FromRoute] int id,
            [FromBody] Sections updatedSection)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingSection = await GetSectionById(id);
            if (existingSection == null)
                return NotFound();

            existingSection.section_name = updatedSection.section_name;
            existingSection.territory_id = updatedSection.territory_id;
            existingSection.section_area = updatedSection.section_area;
            existingSection.cutting_age = updatedSection.cutting_age;
            existingSection.fire_hazard_level_id = updatedSection.fire_hazard_level_id;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Update failed: {ex.Message}");
            }
        }

        // DELETE: api/sections/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSection([FromRoute] int id)
        {
            var section = await _context.Sections.FindAsync(id);
            if (section == null)
                return NotFound();

            try
            {
                _context.Sections.Remove(section);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Delete failed: {ex.Message}");
            }
        }

        // Вспомогательный метод для GET by ID
        [HttpGet("{id}")]
        public async Task<Sections> GetSectionById(int id)
        {
            var section = await _context.Sections.FindAsync(id);
            return section;
        }
    }
}
