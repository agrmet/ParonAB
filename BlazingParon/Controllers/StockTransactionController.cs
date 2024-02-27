using BlazingParon.Models;
using BlazingParon.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingParon.Controllers
{
    [Route("stocktransactionsAPI")]
    [ApiController]
    public class StockTransactionController : Controller
    {
        private readonly InventoryManagementSystemDB _db;
        public StockTransactionController(InventoryManagementSystemDB db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<List<StockTransaction>> GetStockTransactionsAsync()
        {
            return await _db.StockTransactions.ToListAsync();
        }

        [HttpGet("{ProductId}/{SendingWarehouseId}/{ReceivingWarehouseId}")]
        public async Task<StockTransaction?> GetStockTransactionAsync(int ProductId, int SendingWarehouseId, int ReceivingWarehouseId)
        {
            return await _db.StockTransactions.FindAsync(ProductId, SendingWarehouseId, ReceivingWarehouseId);
        }

        [HttpPost]
        public async Task<StockTransaction> AddStockTransactionAsync(StockTransaction stockTransaction)
        {
            _db.StockTransactions.Add(stockTransaction);
            await _db.SaveChangesAsync();
            return stockTransaction;
        }

        [HttpPut]
        public async Task<StockTransaction> UpdateStockTransactionAsync(StockTransaction stockTransaction)
        {
            _db.Entry(stockTransaction).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return stockTransaction;
        }

        [HttpDelete("{ProductId}/{SendingWarehouseId}/{ReceivingWarehouseId}")]
        public async Task DeleteStockTransactionAsync(int ProductId, int SendingWarehouseId, int ReceivingWarehouseId)
        {
            var stockTransaction = await _db.StockTransactions.FindAsync(ProductId, SendingWarehouseId, ReceivingWarehouseId);
            if (stockTransaction == null) return;
            _db.StockTransactions.Remove(stockTransaction);
            await _db.SaveChangesAsync();
        }

    }
}