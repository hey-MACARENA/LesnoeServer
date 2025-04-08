using LesnoeServer.DTO;
using LesnoeServer.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LesnoeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Controller : ControllerBase
    {
        private readonly AppDbContext _context;

        public Controller(AppDbContext context)
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
        public async Task<IActionResult> GetEmpltyAsync()
        {

            var items = new List<int>();

            var response = new
            {
                crudUrl = "/",
                idName = "id",
                columns = _columns,
                rows = items,
                totalRows = items.Count
            };

            return Ok(response);
        }
    }
}
