using LesnoeServer.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace LesnoeServer.Controllers
{
    [ApiController]
    [Route("api/works")]
    public class WorksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WorksController(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: apt/works
        [HttpGet]
        public async Task<List<WorksDetails>> GetWorksAsync(DateOnly? startDate = null, DateOnly? endDate = null, string? sort = null)
        {
            var startParam = new SqlParameter("@startDate", startDate ?? (object)DBNull.Value);
            var endParam = new SqlParameter("@endDate", endDate ?? (object)DBNull.Value);
            var sortParam = new SqlParameter("@sort", sort ?? (object)DBNull.Value);

            return await _context.Set<WorksDetails>()
                                 .FromSqlRaw("EXEC GetWorksWithEmployees @startDate, @endDate, @sort", startParam, endParam, sortParam)
                                 .ToListAsync();
        }

        // POST: api/works
        [HttpPost]
        public async Task<ActionResult<Works>> PostWork([FromBody] Works work)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _context.Works.Add(work);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetWorkById),
                    new { id = work.work_id },
                    work);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/works/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWork(
            [FromRoute] int id,
            [FromBody] Works updatedWork)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingWork = await GetWorkById(id);
            if (existingWork == null)
                return NotFound();

            existingWork.work_date = updatedWork.work_date;
            existingWork.work_type_id = updatedWork.work_type_id;
            existingWork.work_description = updatedWork.work_description;
            existingWork.section_id = updatedWork.section_id;

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

        // DELETE: api/works/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWork([FromRoute] int id)
        {
            var work = await _context.Works.FindAsync(id);
            if (work == null)
                return NotFound();

            try
            {
                _context.Works.Remove(work);
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
        public async Task<Works> GetWorkById(int id)
        {
            var work = await _context.Works.FindAsync(id);
            return work;
        }
    }
}
