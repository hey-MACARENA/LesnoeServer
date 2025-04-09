using LesnoeServer.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace LesnoeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SectionsController(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public async Task<IActionResult> GetSectionsAsync()
        {
            var sections = await _context.Sections.ToListAsync();
            return Ok(sections);
        }

        [HttpGet("{id}")]
        public async Task<Sections> GetSectionById(int id)
        {
            var section = await _context.Sections.FindAsync(id);
            return section == null ? new Sections() : section;
        }

        [HttpPost]
        public async Task<ActionResult<SectionsDetailsWithIds>> PostSection([FromBody] SectionsDetailsWithIds sectionDetails)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var newSection = new Sections();
                newSection.section_name = sectionDetails.section_name;
                newSection.territory_id = sectionDetails.territory_id;
                newSection.section_area = sectionDetails.section_area;
                newSection.fire_hazard_level_id = sectionDetails.fire_hazard_level_id;
                newSection.cutting_age = sectionDetails.cutting_age;

                _context.Sections.Add(newSection);

                await _context.SaveChangesAsync();

                foreach (var empId in sectionDetails.employees)
                {
                    var employee = await _context.Employees.FindAsync(empId);
                    if (employee != null)
                    {
                        employee.section_id = newSection.section_id;
                    }
                }
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetSectionById),
                    new { id = sectionDetails.section_id },
                    sectionDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSection(
            [FromRoute] int id,
            [FromBody] SectionsDetailsWithIds updatedSectionDetails)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingSection = await GetSectionById(id);
            if (existingSection == null)
                return NotFound();

            existingSection.section_name = updatedSectionDetails.section_name;
            existingSection.territory_id = updatedSectionDetails.territory_id;
            existingSection.section_area = updatedSectionDetails.section_area;
            existingSection.cutting_age = updatedSectionDetails.cutting_age;
            existingSection.fire_hazard_level_id = updatedSectionDetails.fire_hazard_level_id;

            var employees = await _context.Employees.Where(e => e.section_id == id).ToListAsync();
            foreach (var e in employees)
                e.section_id = null;

            try
            {
                await _context.SaveChangesAsync();

                foreach (var empId in updatedSectionDetails.employees)
                {
                    var employee = await _context.Employees.FindAsync(empId);
                    if (employee != null)
                    {
                        employee.section_id = existingSection.section_id;
                    }
                }
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Update failed: {ex.Message}");
            }
        }

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
    }
}
