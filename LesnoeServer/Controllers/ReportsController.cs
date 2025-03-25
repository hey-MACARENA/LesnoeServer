using LesnoeServer.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace LesnoeServer.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReportsController(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: apt/reports
        [HttpGet]
        public async Task<IActionResult> GetReportsAsync(bool? IncludeOnlyOvertime = false, string? sort = null)
        {
            var IncludeParam = new SqlParameter("@IncludeOnlyOvertime", IncludeOnlyOvertime ?? (object)DBNull.Value);
            var sortParam = new SqlParameter("@sort", sort ?? (object)DBNull.Value);

            var reports = await _context.Set<ReportsDetails>()
                                 .FromSqlRaw("EXEC GetReports @IncludeOnlyOvertime, @sort", IncludeParam, sortParam)
                                 .ToListAsync();

            var response = new
            {
                Data = reports,
                Count = reports.Count
            };

            return Ok(response);
        }

        // POST: api/reports
        [HttpPost]
        public async Task<ActionResult<Reports>> PostEmployee([FromBody] Reports report)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _context.Reports.Add(report);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetReportById),
                    new { id = report.report_id },
                    report);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/reports/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReport(
            [FromRoute] int id,
            [FromBody] Reports updatedReport)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingReport = await GetReportById(id);
            if (existingReport == null)
                return NotFound();

            existingReport.work_id = updatedReport.work_id;
            existingReport.hourly_rate = updatedReport.hourly_rate;
            existingReport.norm_hours = updatedReport.norm_hours;
            existingReport.overtime_hours = updatedReport.overtime_hours;

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

        // DELETE: api/reports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport([FromRoute] int id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null)
                return NotFound();

            try
            {
                _context.Reports.Remove(report);
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
        public async Task<Reports> GetReportById(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            return report;
        }
    }
}
