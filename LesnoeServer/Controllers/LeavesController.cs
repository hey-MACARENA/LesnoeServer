using LesnoeServer.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace LesnoeServer.Controllers
{
    [ApiController]
    [Route("api/leaves")]
    public class LeavesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LeavesController(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: apt/leaves
        [HttpGet]
        public async Task<IActionResult> GetLeavesAsync(DateOnly? startDate = null, DateOnly? endDate = null, string? sort = null)
        {
            var startParam = new SqlParameter("@startDate", startDate ?? (object)DBNull.Value);
            var endParam = new SqlParameter("@endDate", endDate ?? (object)DBNull.Value);
            var sortParam = new SqlParameter("@sort", sort ?? (object)DBNull.Value);

            var leaves = await _context.Set<LeavesDetails>()
                                 .FromSqlRaw("EXEC GetLeaves @startDate, @endDate, @sort", startParam, endParam, sortParam)
                                 .ToListAsync();

            var response = new
            {
                Data = leaves,
                Count = leaves.Count
            };

            return Ok(response);
        }

        // POST: api/leaves
        [HttpPost]
        public async Task<ActionResult<Leaves>> PostEmployee([FromBody] Leaves leave)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _context.Leaves.Add(leave);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetLeaveById),
                    new { id = leave.leave_id },
                    leave);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/leaves/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeave(
            [FromRoute] int id,
            [FromBody] Leaves updatedLeave)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingLeave = await GetLeaveById(id);
            if (existingLeave == null)
                return NotFound();

            existingLeave.employee_id = updatedLeave.employee_id;
            existingLeave.leave_type_id = updatedLeave.leave_type_id;
            existingLeave.start_date = updatedLeave.start_date;
            existingLeave.end_date = updatedLeave.end_date;

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

        // DELETE: api/leaves/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeave([FromRoute] int id)
        {
            var leave = await _context.Leaves.FindAsync(id);
            if (leave == null)
                return NotFound();

            try
            {
                _context.Leaves.Remove(leave);
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
        public async Task<Leaves> GetLeaveById(int id)
        {
            var leave = await _context.Leaves.FindAsync(id);
            return leave;
        }
    }
}
