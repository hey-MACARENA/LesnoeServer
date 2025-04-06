using LesnoeServer.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LesnoeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LeaveTypesController(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public async Task<IActionResult> GetLeavesAsync()
        {
            var leavesTypes = await _context.Leave_types.ToListAsync();
            return Ok(leavesTypes);
        }

        [HttpGet("{id}")]
        public async Task<Leave_types> GetLeaveById(int id)
        {
            var leaveType = await _context.Leave_types.FindAsync(id);
            return leaveType == null ? new Leave_types() : leaveType;
        }

        [HttpPost]
        public async Task<ActionResult<Leave_types>> PostEmployee([FromBody] Leave_types leaveType)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _context.Leave_types.Add(leaveType);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetLeaveById),
                    new { id = leaveType.leave_type_id },
                    leaveType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeave(
            [FromRoute] int id,
            [FromBody] Leave_types updatedLeaveType)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingLeaveType = await GetLeaveById(id);
            if (existingLeaveType == null)
                return NotFound();

            existingLeaveType.leave_type = updatedLeaveType.leave_type;

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
            var leaveType = await _context.Leave_types.FindAsync(id);
            if (leaveType == null)
                return NotFound();

            try
            {
                _context.Leave_types.Remove(leaveType);
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
