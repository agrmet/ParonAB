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
        [HttpGet("{ProductId}/{WarehouseId}")]
        public async Task<InventoryCount?> GetInventoryCountAsync(int ProductId, int WarehouseId)
        {
            return await _db.InventoryCounts.FindAsync(ProductId, WarehouseId);
        }
        [HttpPost]
        public async Task<InventoryCount> AddInventoryCountAsync(InventoryCount inventorycount)
        {
            _db.InventoryCounts.Add(inventorycount);
            await _db.SaveChangesAsync();
            return inventorycount;
        }
        [HttpPut]
        public async Task<InventoryCount> UpdateInventoryCountAsync(InventoryCount inventorycount)
        {
            _db.Entry(inventorycount).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return inventorycount;
        }
        [HttpDelete("{ProductId}/{WarehouseId}")]
        public async Task DeleteInventoryCountAsync(int ProductId, int WarehouseId)
        {
            var inventorycount = await _db.InventoryCounts.FindAsync(ProductId, WarehouseId);
            if (inventorycount == null) return;
            _db.InventoryCounts.Remove(inventorycount);
            await _db.SaveChangesAsync();
        }

    }
}