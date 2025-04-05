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
    public class LeavesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LeavesController(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public async Task<IActionResult> GetLeavesAsync(DateOnly? startDate = null, DateOnly? endDate = null, string? sort = null)
        {
            var leaves = await _context.Leaves.ToListAsync();
            return Ok(leaves);
        }

        [HttpGet("{id}")]
        public async Task<Leaves> GetLeaveById(int id)
        {
            var leave = await _context.Leaves.FindAsync(id);
            return leave == null ? new Leaves() : leave;
        }

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
    }
}
