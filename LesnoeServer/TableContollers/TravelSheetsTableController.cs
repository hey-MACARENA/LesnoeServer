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
                new ColumnDto("departure_date", "departure_date", "Дата отправки", "date", true, new SettingsDto()),
                new ColumnDto("vehicle_name", "vehicle_name", "Транспорт", "text", true, new SettingsDto(maxChar: 30)),
                new ColumnDto("driver_name", "driver_id", "Водитель", "select", true, new SettingsDto(url: "/drivers")),
                new ColumnDto("departure_mileage", "departure_mileage", "Километраж выезда", "number", true, new SettingsDto(maxNum: 9999999)),
                new ColumnDto("return_mileage", "return_mileage", "Километраж возврата", "number", true, new SettingsDto(maxNum: 9999999)),
                new ColumnDto("departure_fuel", "departure_fuel", "Торливо выезда", "number", true, new SettingsDto(maxNum: 9999999)),
                new ColumnDto("return_fuel", "return_fuel", "Торливо возврата", "number", true, new SettingsDto(maxNum: 9999999)),
                new ColumnDto("fuel_rate", "fuel_rate", "Расход топлива", "number", true, new SettingsDto(maxNum: 1, intOnly: false)),
                new ColumnDto("actual_fuel_consumption", "actual_fuel_consumption", "Фактическиие расход топлива", "number", true, new SettingsDto(maxNum: 1, intOnly: false)),
            ];
        }

        private List<ColumnDto> _columns;

        [HttpGet]
        public async Task<IActionResult> GetTravelSheetsAsync(DateOnly? startDate = null, DateOnly? endDate = null, string? sort = null)
        {
            var startDateParam = new SqlParameter("@startDate", startDate ?? (object)DBNull.Value);
            var endDateParam = new SqlParameter("@endDate", endDate ?? (object)DBNull.Value);
            var sortParam = new SqlParameter("@sort", sort ?? (object)DBNull.Value);

            var items = await _context.Set<Travel_sheetsDetails>()
                                  .FromSqlRaw("EXEC GetTravelSheets @startDate, @endDate, @sort", startDateParam, endDateParam, sortParam)
                                  .ToListAsync();

            var response = new
            {
                crudUrl = "/travelsheets",
                idName = "travel_sheet_id",
                columns = _columns,
                rows = items,
                totalRows = items.Count
            };

            return Ok(response);
        }
    }
}
