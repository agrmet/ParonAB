using BlazingParon.Models;
using BlazingParon.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingParon.Controllers
{
    [Route("deliveriesAPI")]
    [ApiController]
    public class DeliveriesController : Controller
    {
        private readonly InventoryManagementSystemDB _db;
        public DeliveriesController(InventoryManagementSystemDB db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<List<Deliveries>> GetDeliveriesAsync()
        {
            return await _db.Deliveries.ToListAsync();
        }

        [HttpGet("{ProductId}/{SendingWarehouseId}/{ReceivingWarehouseId}")]
        public async Task<Deliveries?> GetDeliveriesAsync(int ProductId, int SendingWarehouseId, int ReceivingWarehouseId)
        {
            return await _db.Deliveries.FindAsync(ProductId, SendingWarehouseId, ReceivingWarehouseId);
        }

        [HttpPost]
        public async Task<Deliveries> AddDeliveriesAsync(Deliveries Deliveries)
        {
            _db.Deliveries.Add(Deliveries);
            await _db.SaveChangesAsync();
            return Deliveries;
        }

        [HttpPut]
        public async Task<Deliveries> UpdateDeliveriesAsync(Deliveries Deliveries)
        {
            _db.Entry(Deliveries).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return Deliveries;
        }

        [HttpDelete("{ProductId}/{SendingWarehouseId}/{ReceivingWarehouseId}")]
        public async Task DeleteDeliveriesAsync(int ProductId, int SendingWarehouseId, int ReceivingWarehouseId)
        {
            var Deliveries = await _db.Deliveries.FindAsync(ProductId, SendingWarehouseId, ReceivingWarehouseId);
            if (Deliveries == null) return;
            _db.Deliveries.Remove(Deliveries);
            await _db.SaveChangesAsync();
        }

    }
}