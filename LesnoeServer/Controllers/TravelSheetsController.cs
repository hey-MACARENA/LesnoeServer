using LesnoeServer.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace LesnoeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelSheetsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TravelSheetsController(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public async Task<IActionResult> GetTravel_sheetsAsync(DateOnly? startDate = null, DateOnly? endDate = null, string? sort = null)
        {
            var travelSheet = await _context.Travel_sheets.ToListAsync();
            return Ok(travelSheet);
        }

        [HttpGet("{id}")]
        public async Task<Travel_sheets> GetTravel_sheetById(int id)
        {
            var travel_sheet = await _context.Travel_sheets.FindAsync(id);
            return travel_sheet == null ? new Travel_sheets() : travel_sheet;
        }

        [HttpPost]
        public async Task<ActionResult<Travel_sheets>> PostTravel_sheet([FromBody] Travel_sheets Travel_sheet)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _context.Travel_sheets.Add(Travel_sheet);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetTravel_sheetById),
                    new { id = Travel_sheet.travel_sheet_id },
                    Travel_sheet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTravel_sheet(
            [FromRoute] int id,
            [FromBody] Travel_sheets updatedTravel_sheet)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingTravel_sheet = await GetTravel_sheetById(id);
            if (existingTravel_sheet == null)
                return NotFound();

            existingTravel_sheet.departure_date = updatedTravel_sheet.departure_date;
            existingTravel_sheet.vehicle_name = updatedTravel_sheet.vehicle_name;
            existingTravel_sheet.driver_id = updatedTravel_sheet.driver_id;
            existingTravel_sheet.departure_mileage = updatedTravel_sheet.departure_mileage;
            existingTravel_sheet.return_mileage = updatedTravel_sheet.return_mileage;
            existingTravel_sheet.departure_fuel = updatedTravel_sheet.departure_fuel;
            existingTravel_sheet.return_fuel = updatedTravel_sheet.return_fuel;
            existingTravel_sheet.fuel_rate = updatedTravel_sheet.fuel_rate;
            existingTravel_sheet.actual_fuel_consumption = updatedTravel_sheet.actual_fuel_consumption;

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTravel_sheet([FromRoute] int id)
        {
            var travel_sheet = await _context.Travel_sheets.FindAsync(id);
            if (travel_sheet == null)
                return NotFound();

            try
            {
                _context.Travel_sheets.Remove(travel_sheet);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Delete failed: {ex.Message}");
            }
        }
    }
}
