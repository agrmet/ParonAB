using BlazingParon.Models;
using BlazingParon.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingParon.Controllers
{

    [Route("productsAPI")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly InventoryManagementSystemDB _db;
        public ProductController(InventoryManagementSystemDB db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<List<Product>> GetProductsAsync()
        {
            return await _db.Products.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<Product?> GetProductAsync(int id)
        {
            return await _db.Products.FindAsync(id);
        }
        [HttpPost]
        public async Task<Product> AddProductAsync(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return product;
        }
        [HttpPut]
        public async Task<Product> UpdateProductAsync(Product product)
        {
            _db.Entry(product).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return product;
        }
        [HttpDelete("{id}")]
        public async Task DeleteProductAsync(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null) return;
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
        }

    }
}