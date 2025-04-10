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
                new ColumnDTO("name", "name", "Имя", "text", true, new SettingsDTO(maxChar: 30)),
                new ColumnDTO("position_name", "position_id", "Должность", "select", true, new SettingsDTO(url: "/positions")),
                new ColumnDTO("section_name", "section_id", "Квартал", "select", false, new SettingsDTO(url: "/sections")),
                new ColumnDTO("team_name", "team_id", "Бригада", "select", false, new SettingsDTO(url: "/teams")),
                new ColumnDTO("work_experience", "work_experience", "Опыт", "number", true, new SettingsDTO(maxNum: 99)),
                new ColumnDTO("residence", "residence", "Адрес", "text", true, new SettingsDTO(maxChar: 50)),
            ];

            _filters = [
                new FiltersDTO("teamId", "Бригада", "select", new SettingsDTO(url: "/teams", allowAll: true)),
            ];
        }

        private List<ColumnDTO> _columns;
        private List<FiltersDTO> _filters;

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
