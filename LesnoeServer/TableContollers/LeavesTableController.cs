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
                new ColumnDTO("employee_name", "employee_id", "Имя сотрудника", "select", true, new SettingsDTO(url: "/employees")),
                new ColumnDTO("leave_type_name", "leave_type_id", "Вид отпуска", "select", true, new SettingsDTO(url: "/leavetypes")),
                new ColumnDTO("start_date", "start_date", "Дата начала", "start_date", true, new SettingsDTO()),
                new ColumnDTO("end_date", "end_date", "Дата окончания", "end_date", true, new SettingsDTO()),
            ];

            _filters = [
                new FiltersDTO("start_date", "Время", "start_date", new SettingsDTO()),
                new FiltersDTO("end_date", "Время", "end_date", new SettingsDTO()),
            ];
        }

        private List<ColumnDTO> _columns;
        private List<FiltersDTO> _filters;

        [HttpGet]
        public async Task<IActionResult> GetLeavesAsync(DateOnly? start_date = null, DateOnly? end_date = null, string? sort = null)
        {
            var startDateParam = new SqlParameter("@startDate", start_date ?? (object)DBNull.Value);
            var endDateParam = new SqlParameter("@endDate", end_date ?? (object)DBNull.Value);
            var sortParam = new SqlParameter("@sort", sort ?? (object)DBNull.Value);

            var items = await _context.Set<LeavesDetails>()
                                  .FromSqlRaw("EXEC GetLeaves @startDate, @endDate, @sort", startDateParam, endDateParam, sortParam)
                                  .ToListAsync();

            var response = new
            {
                crudUrl = "/leaves",
                idName = "leave_id",
                columns = _columns,
                filters = _filters,
                rows = items,
                totalRows = items.Count
            };

            return Ok(response);
        }
    }
}
