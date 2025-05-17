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
    public class SectionsTableController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SectionsTableController(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            _columns = [
                new ColumnDTO("section_name", "section_name", "Название", "text", true, true, "", new SettingsDTO(maxChar: 30)),
                new ColumnDTO("territory_type", "territory_id", "Тип пород", "select", true, false, "select", new SettingsDTO(url: "/territories")),
                new ColumnDTO("section_area", "section_area", "Площадь", "number", true, true, "", new SettingsDTO(maxNum: 999999999)),
                new ColumnDTO("cutting_age", "cutting_age", "Возраст", "number", true, true, "", new SettingsDTO(maxNum: 9999)),
                new ColumnDTO("fire_hazard_level", "fire_hazard_level_id", "Уровень пожарной опасности", "select", true, false, "select", new SettingsDTO(url: "/firehazardlevels")),
                new ColumnDTO("employees", "employees", "Сорудники", "list", false, true, "", new SettingsDTO(url: "/employees")),
            ];
        }

        private List<ColumnDTO> _columns;

        [HttpGet]
        public async Task<IActionResult> GetSectionsAsync(string? sort = null)
        {
            var sortParam = new SqlParameter("@sort", sort ?? (object)DBNull.Value);

            var rawItems = await _context.Set<SectionsDetailsRaw>()
                             .FromSqlRaw("EXEC GetSectionsWithEmployees @sort", sortParam)
                             .ToListAsync();

            var transformedSections = rawItems
                .GroupBy(s => new
                {
                    s.section_id,
                    s.section_name,
                    s.territory_id,
                    s.territory_type,
                    s.section_area,
                    s.cutting_age,
                    s.fire_hazard_level_id,
                    s.fire_hazard_level
                })
                .Select(g =>
                {
                    var firstItem = g.First();
                    return new SectionsDetails
                    {
                        section_id = firstItem.section_id,
                        section_name = firstItem.section_name,
                        territory_id = firstItem.territory_id,
                        territory_type = firstItem.territory_type,
                        section_area = firstItem.section_area,
                        cutting_age = firstItem.cutting_age,
                        fire_hazard_level_id = firstItem.fire_hazard_level_id,
                        fire_hazard_level = firstItem.fire_hazard_level,
                        employees = g.Where(x => x.employee_id.HasValue)
                                     .Select(x => new EmployeeDTO
                                     {
                                         employee_id = x.employee_id,
                                         employee_name = x.employee_name
                                     }).ToList()
                    };
                }).ToList();

            var response = new
            {
                crudUrl = "/sections",
                idName = "section_id",
                columns = _columns,
                rows = transformedSections,
                totalRows = transformedSections.Count
            };

            return Ok(response);
        }
    }
}
