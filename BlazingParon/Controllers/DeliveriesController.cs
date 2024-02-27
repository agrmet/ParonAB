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

        [HttpGet("{deliveryId}")]
        public async Task<Deliveries?> GetDeliveriesAsync(int deliveryId)
        {
            return await _db.Deliveries.FindAsync(deliveryId);
        }

        [HttpPost]
        public async Task<Deliveries> AddDeliveriesAsync(Deliveries delivery)
        {
            if (delivery is null) throw new ArgumentNullException(nameof(delivery));
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

            var existingDelivery = await _db.Deliveries.FindAsync(delivery.DeliveryId);
            if (existingDelivery == null)
            {
                throw new Exception("Delivery not found");
            }

            _db.Entry(existingDelivery).CurrentValues.SetValues(delivery);
            await UpdateInventory(delivery);
            await _db.SaveChangesAsync();

            return existingDelivery;
        }


        [HttpDelete("{deliveryId}")]
        public async Task DeleteDeliveriesAsync(int deliveryId)
        {
            var delivery = await _db.Deliveries.FindAsync(deliveryId);
            if (delivery == null) throw new Exception("Delivery not found");
            await DeleteInventory(delivery);
            _db.Deliveries.Remove(delivery);
            await _db.SaveChangesAsync();
        }

        private async Task AddInventory(Deliveries delivery)
        {
            var sendingInventory = await _db.InventoryCounts.FindAsync(delivery.ProductId, delivery.SendingWarehouseId);
            var receivingInventory = await _db.InventoryCounts.FindAsync(delivery.ProductId, delivery.ReceivingWarehouseId);
            var product = await _db.Products.FindAsync(delivery.ProductId);
            var sendingWarehouse = await _db.Warehouses.FindAsync(delivery.SendingWarehouseId);
            var receivingWarehouse = await _db.Warehouses.FindAsync(delivery.ReceivingWarehouseId);

            if (sendingInventory is null) throw new Exception("Item not in sending inventory");
            if (product is null) throw new Exception("Product not found");
            if (sendingWarehouse is null || receivingWarehouse is null) throw new Exception("Warehouse not found");
            if (receivingInventory is null)
            {
                receivingInventory = new InventoryCount
                {
                    ProductId = delivery.ProductId,
                    WarehouseId = delivery.ReceivingWarehouseId,
                    Quantity = delivery.Quantity
                };
                _db.InventoryCounts.Add(receivingInventory);
                await _db.SaveChangesAsync();
            }
            else
            {
                receivingInventory.Quantity += delivery.Quantity;
                _db.Entry(receivingInventory).State = EntityState.Modified;
            }
            if (sendingInventory.Quantity < delivery.Quantity) throw new Exception("Not enough inventory");

            sendingInventory.Quantity -= delivery.Quantity;

            _db.Entry(sendingInventory).State = EntityState.Modified;
            await _db.SaveChangesAsync();

        }


        private async Task UpdateInventory(Deliveries delivery)
        {
            var oldDelivery = await _db.Deliveries.FindAsync(delivery.DeliveryId);
            var sendingInventory = await _db.InventoryCounts.FindAsync(delivery.ProductId, delivery.SendingWarehouseId);
            var receivingInventory = await _db.InventoryCounts.FindAsync(delivery.ProductId, delivery.ReceivingWarehouseId);
            var product = await _db.Products.FindAsync(delivery.ProductId);
            var sendingWarehouse = await _db.Warehouses.FindAsync(delivery.SendingWarehouseId);
            var receivingWarehouse = await _db.Warehouses.FindAsync(delivery.ReceivingWarehouseId);

            if (sendingWarehouse is null || receivingWarehouse is null) throw new Exception("Warehouse not found");
            if (sendingInventory is null) throw new Exception("Inventory not found");
            if (product is null) throw new Exception("Product not found");
            if (oldDelivery == null) throw new Exception("Delivery not found");

            await DeleteInventory(oldDelivery);
            await AddInventory(delivery);
        }

        private async Task DeleteInventory(Deliveries delivery)
        {
            var sendingInventory = await _db.InventoryCounts.FindAsync(delivery.ProductId, delivery.SendingWarehouseId);
            var receivingInventory = await _db.InventoryCounts.FindAsync(delivery.ProductId, delivery.ReceivingWarehouseId);
            var sendingWarehouse = await _db.Warehouses.FindAsync(delivery.SendingWarehouseId);
            var receivingWarehouse = await _db.Warehouses.FindAsync(delivery.ReceivingWarehouseId);

            if (sendingWarehouse == null || receivingWarehouse == null) throw new Exception("Warehouse not found");
            if (sendingInventory == null || receivingInventory == null) throw new Exception("Inventory not found");

            sendingInventory.Quantity += delivery.Quantity;
            receivingInventory.Quantity -= delivery.Quantity;

            _db.Entry(sendingInventory).State = EntityState.Modified;
            _db.Entry(receivingInventory).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

    }
}