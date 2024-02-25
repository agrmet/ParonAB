using Microsoft.EntityFrameworkCore;
namespace BlazingParon.Models

{
    public class Product
    {
        public int ProductId { get; set; }
        public int Price { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}