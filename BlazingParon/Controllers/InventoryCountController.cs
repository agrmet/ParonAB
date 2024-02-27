using BlazingParon.Models;
using BlazingParon.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingParon.Controllers
{

    [Route("inventorycountAPI")]
    [ApiController]

    public class InventoryCountController : Controller
    {
        private readonly InventoryManagementSystemDB _db;
        public InventoryCountController(InventoryManagementSystemDB db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<List<InventoryCount>> GetInventoryCountsAsync()
        {
            return await _db.InventoryCounts.ToListAsync();
        }
    }
}