using LesnoeServer.DTO;
using LesnoeServer.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LesnoeServer.TableContollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeavesTableController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LeavesTableController(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            _columns = [
                new ColumnDto("employee_name", "Имя сотрудника", "text", true, new SettingsDto(maxChar: 30)),
                new ColumnDto("leave_type_name", "Вид отпуска", "select", true, new SettingsDto(url: "api/leaves")),
                new ColumnDto("start_date", "Дата начала", "date", true, new SettingsDto()),
                new ColumnDto("end_date", "Дата окончания", "date", true, new SettingsDto()),
            ];
        }

        private List<ColumnDto> _columns;

        [HttpGet]
        public async Task<IActionResult> GetLeavesAsync(DateOnly? startDate = null, DateOnly? endDate = null, string? sort = null)
        {
            var startDateParam = new SqlParameter("@startDate", startDate ?? (object)DBNull.Value);
            var endDateParam = new SqlParameter("@endDate", endDate ?? (object)DBNull.Value);
            var sortParam = new SqlParameter("@sort", sort ?? (object)DBNull.Value);

            var items = await _context.Set<LeavesDetails>()
                                  .FromSqlRaw("EXEC GetLeaves @startDate, @endDate, @sort", startDateParam, endDateParam, sortParam)
                                  .ToListAsync();

            var response = new
            {
                idName = "leave_id",
                columns = _columns,
                rows = items,
                totalRows = items.Count
            };

            return Ok(response);
        }
    }
}
