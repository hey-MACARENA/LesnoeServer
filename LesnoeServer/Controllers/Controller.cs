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

            _columns = [];
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
