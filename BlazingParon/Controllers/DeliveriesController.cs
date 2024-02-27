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
        public async Task<Deliveries> AddDeliveriesAsync(Deliveries delivery)
        {
            if (delivery.Quantity < 0) throw new Exception("Quantity cannot be negative");
            AddInventory(delivery);
            _db.Deliveries.Add(delivery);
            await _db.SaveChangesAsync();
            return delivery;
        }

        [HttpPut]
        public async Task<Deliveries> UpdateDeliveriesAsync(Deliveries delivery)
        {
            if (delivery.Quantity < 0) throw new Exception("Quantity cannot be negative");
            UpdateInventory(delivery);
            _db.Entry(delivery).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return delivery;
        }

        [HttpDelete("{ProductId}/{SendingWarehouseId}/{ReceivingWarehouseId}")]
        public async Task DeleteDeliveriesAsync(int ProductId, int SendingWarehouseId, int ReceivingWarehouseId)
        {
            var delivery = await _db.Deliveries.FindAsync(ProductId, SendingWarehouseId, ReceivingWarehouseId);
            if (delivery == null) return;
            DeleteInventory(delivery);
            _db.Deliveries.Remove(delivery);
            await _db.SaveChangesAsync();
        }

        private void AddInventory(Deliveries delivery)
        {
            var sendingWarehouse = _db.InventoryCounts.Find(delivery.ProductId, delivery.SendingWarehouseId);
            var receivingWarehouse = _db.InventoryCounts.Find(delivery.ProductId, delivery.ReceivingWarehouseId);
            if (delivery.Quantity > sendingWarehouse.Quantity) throw new Exception("Not enough inventory");
            if (sendingWarehouse == null || receivingWarehouse == null) return;

            sendingWarehouse.Quantity -= delivery.Quantity;
            receivingWarehouse.Quantity += delivery.Quantity;

            _db.Entry(sendingWarehouse).State = EntityState.Modified;
            _db.Entry(receivingWarehouse).State = EntityState.Modified;
        }

        private void UpdateInventory(Deliveries delivery)
        {
            var oldDelivery = _db.Deliveries.Find(delivery.ProductId, delivery.SendingWarehouseId, delivery.ReceivingWarehouseId);
            var sendingWarehouse = _db.InventoryCounts.Find(delivery.ProductId, delivery.SendingWarehouseId);
            var receivingWarehouse = _db.InventoryCounts.Find(delivery.ProductId, delivery.ReceivingWarehouseId);

            if (delivery.Quantity > oldDelivery.Quantity)
            {
                sendingWarehouse.Quantity -= (delivery.Quantity - oldDelivery.Quantity);
                receivingWarehouse.Quantity += (delivery.Quantity - oldDelivery.Quantity);
            }
            else
            {
                sendingWarehouse.Quantity += (oldDelivery.Quantity - delivery.Quantity);
                receivingWarehouse.Quantity -= (oldDelivery.Quantity - delivery.Quantity);
            }
            if (sendingWarehouse.Quantity < 0) throw new Exception("Not enough inventory");
            if (sendingWarehouse == null || receivingWarehouse == null) return;

            _db.Entry(sendingWarehouse).State = EntityState.Modified;
            _db.Entry(receivingWarehouse).State = EntityState.Modified;
        }

        private void DeleteInventory(Deliveries delivery)
        {
            var sendingWarehouse = _db.InventoryCounts.Find(delivery.ProductId, delivery.SendingWarehouseId);
            var receivingWarehouse = _db.InventoryCounts.Find(delivery.ProductId, delivery.ReceivingWarehouseId);

            if (sendingWarehouse == null || receivingWarehouse == null) return;

            sendingWarehouse.Quantity += delivery.Quantity;
            receivingWarehouse.Quantity -= delivery.Quantity;

            _db.Entry(sendingWarehouse).State = EntityState.Modified;
            _db.Entry(receivingWarehouse).State = EntityState.Modified;
        }

    }
}