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
    public class EmployeesTableController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeesTableController(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            _columns = [
                new ColumnDto("name", "name", "Имя", "text", true, new SettingsDto(maxChar: 30)),
                new ColumnDto("position_name", "position_id", "Должность", "select", true, new SettingsDto(url: "/positions")),
                new ColumnDto("section_name", "section_id", "Квартал", "select", false, new SettingsDto(url: "/sections")),
                new ColumnDto("team_name", "team_id", "Бригада", "select", false, new SettingsDto(url: "/teams")),
                new ColumnDto("work_experience", "work_experience", "Опыт", "number", true, new SettingsDto(maxNum: 99)),
                new ColumnDto("residence", "residence", "Адрес", "text", true, new SettingsDto(maxChar: 50)),
            ];

            _filters = [
                new FiltersDto("teamId", "Бригада", "select", new SettingsDto(url: "/teams")),
            ];
        }

        private List<ColumnDto> _columns;
        private List<FiltersDto> _filters;

        [HttpGet]
        public async Task<IActionResult> GetEmployeesAsync(int? teamId = null, string? sort = null)
        {
            var teamIdParam = new SqlParameter("@teamId", teamId ?? (object)DBNull.Value);
            var sortParam = new SqlParameter("@sort", sort ?? (object)DBNull.Value);

            var items = await _context.Set<EmployeeDetails>()
                                  .FromSqlRaw("EXEC GetEmployees @teamId, @sort", teamIdParam, sortParam)
                                  .ToListAsync();

            var response = new
            {
                crudUrl = "/employees",
                idName = "employee_id",
                columns = _columns,
                filters = _filters,
                rows = items,
                totalRows = items.Count
            };

            return Ok(response);
        }
    }
}
