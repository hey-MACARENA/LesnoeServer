using LesnoeServer.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace LesnoeServer.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: apt/employees
        [HttpGet]
        public async Task<IActionResult> GetEmployeesAsync(int? teamId = null, string? sort = null)
        {
            var teamIdParam = new SqlParameter("@teamId", teamId ?? (object)DBNull.Value);
            var sortParam = new SqlParameter("@sort", sort ?? (object)DBNull.Value);

            var employees = await _context.Set<EmployeeDetails>()
                                  .FromSqlRaw("EXEC GetEmployees @teamId, @sort", teamIdParam, sortParam)
                                  .ToListAsync();

            var response = new
            {
                Data = employees,
                Count = employees.Count
            };

            return Ok(response);
        }

        // POST: api/employees
        [HttpPost]
        public async Task<ActionResult<Employees>> PostEmployee([FromBody] Employees employee)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetEmployeeById),
                    new { id = employee.employee_id },
                    employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(
            [FromRoute] int id,
            [FromBody] Employees updatedEmployee)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingEmployee = await GetEmployeeById(id);
            if (existingEmployee == null)
                return NotFound();

            existingEmployee.name = updatedEmployee.name;
            existingEmployee.position_id = updatedEmployee.position_id;
            existingEmployee.section_id = updatedEmployee.section_id;
            existingEmployee.team_id = updatedEmployee.team_id;
            existingEmployee.work_experience = updatedEmployee.work_experience;
            existingEmployee.residence = updatedEmployee.residence;

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

        // DELETE: api/employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return NotFound();

            try
            {
                _context.Employees.Remove(employee);
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
        public async Task<Employees> GetEmployeeById(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            return employee;
        }
    }
}