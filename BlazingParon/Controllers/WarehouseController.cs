using BlazingParon.Models;
using BlazingParon.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingParon.Controllers
{

    [Route("warehousesAPI")]
    [ApiController]

    public class WarehouseController : Controller
    {
        private readonly InventoryManagementSystemDB _db;
        public WarehouseController(InventoryManagementSystemDB db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<List<Warehouse>> GetWarehousesAsync()
        {
            return await _db.Warehouses.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<Warehouse?> GetWarehouseAsync(int id)
        {
            return await _db.Warehouses.FindAsync(id);
        }
        [HttpPost]
        public async Task<Warehouse> AddWarehouseAsync(Warehouse warehouse)
        {
            _db.Warehouses.Add(warehouse);
            await _db.SaveChangesAsync();
            return warehouse;
        }
        [HttpPut]
        public async Task<Warehouse> UpdateWarehouseAsync(Warehouse warehouse)
        {
            _db.Entry(warehouse).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return warehouse;
        }
        [HttpDelete("{id}")]
        public async Task DeleteWarehouseAsync(int id)
        {
            var warehouse = await _db.Warehouses.FindAsync(id);
            if (warehouse == null) return;
            _db.Warehouses.Remove(warehouse);
            await _db.SaveChangesAsync();
        }

    }
}