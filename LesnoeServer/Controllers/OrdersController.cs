using LesnoeServer.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace LesnoeServer.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: apt/orders
        [HttpGet]
        public async Task<List<OrdersDetails>> GetOrdersAsync(DateOnly? startDate = null, DateOnly? endDate = null, string? sort = null)
        {
            var startParam = new SqlParameter("@startDate", startDate ?? (object)DBNull.Value);
            var endParam = new SqlParameter("@endDate", endDate ?? (object)DBNull.Value);
            var sortParam = new SqlParameter("@sort", sort ?? (object)DBNull.Value);

            return await _context.Set<OrdersDetails>()
                                 .FromSqlRaw("EXEC GetOrdersWithEmployees @startDate, @endDate, @sort", startParam, endParam, sortParam)
                                 .ToListAsync();
        }

        // POST: api/orders
        [HttpPost]
        public async Task<ActionResult<Orders>> PostEmployee([FromBody] Orders order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetOrderById),
                    new { id = order.order_id },
                    order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(
            [FromRoute] int id,
            [FromBody] Orders updatedEmployee)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingOrder = await GetOrderById(id);
            if (existingOrder == null)
                return NotFound();

            existingOrder.order_type_id = updatedEmployee.order_type_id;
            existingOrder.order_date = updatedEmployee.order_date;
            existingOrder.order_descriction = updatedEmployee.order_descriction;

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

        // DELETE: api/orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                return NotFound();

            try
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Delete failed: {ex.Message}");
            }
        }

        // Вспомогательный метод для GET by ID
        [HttpGet("{id}")]
        public async Task<Orders> GetOrderById(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            return order;
        }
    }
}
