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
    public class TravelSheetsTableController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TravelSheetsTableController(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            _columns = [
                new ColumnDTO("departure_date", "departure_date", "Дата отправки", "date", true, new SettingsDTO()),
                new ColumnDTO("vehicle_name", "vehicle_name", "Транспорт", "text", true, new SettingsDTO(maxChar: 30)),
                new ColumnDTO("driver_name", "driver_id", "Водитель", "select", true, new SettingsDTO(url: "/drivers")),
                new ColumnDTO("departure_mileage", "departure_mileage", "Километраж выезда", "number", true, new SettingsDTO(maxNum: 9999999)),
                new ColumnDTO("return_mileage", "return_mileage", "Километраж возврата", "number", true, new SettingsDTO(maxNum: 9999999)),
                new ColumnDTO("departure_fuel", "departure_fuel", "Торливо выезда", "number", true, new SettingsDTO(maxNum: 9999999)),
                new ColumnDTO("return_fuel", "return_fuel", "Торливо возврата", "number", true, new SettingsDTO(maxNum: 9999999)),
                new ColumnDTO("fuel_rate", "fuel_rate", "Расход топлива", "number", true, new SettingsDTO(maxNum: 1, intOnly: false)),
                new ColumnDTO("actual_fuel_consumption", "actual_fuel_consumption", "Фактическиие расход топлива", "number", true, new SettingsDTO(maxNum: 1, intOnly: false)),
            ];

            _filters = [
                new FiltersDTO("start_date", "Время", "start_date", new SettingsDTO()),
                new FiltersDTO("end_date", "Время", "end_date", new SettingsDTO()),
            ];
        }

        private List<ColumnDTO> _columns;
        private List<FiltersDTO> _filters;

        [HttpGet]
        public async Task<IActionResult> GetTravelSheetsAsync(DateOnly? start_date = null, DateOnly? end_date = null, string? sort = null)
        {
            var startDateParam = new SqlParameter("@startDate", start_date ?? (object)DBNull.Value);
            var endDateParam = new SqlParameter("@endDate", end_date ?? (object)DBNull.Value);
            var sortParam = new SqlParameter("@sort", sort ?? (object)DBNull.Value);

            var items = await _context.Set<Travel_sheetsDetails>()
                                  .FromSqlRaw("EXEC GetTravelSheets @startDate, @endDate, @sort", startDateParam, endDateParam, sortParam)
                                  .ToListAsync();

            var response = new
            {
                crudUrl = "/travelsheets",
                idName = "travel_sheet_id",
                columns = _columns,
                filters = _filters,
                rows = items,
                totalRows = items.Count
            };

            return Ok(response);
        }
    }
}
