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
        }

        private List<ColumnDTO> _columns;

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
