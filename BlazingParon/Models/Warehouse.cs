using Microsoft.EntityFrameworkCore;
namespace BlazingParon.Models

{
    public class Warehouse
    {
        public int WarehouseId { get; set; }
        public string Name { get; set; } = null!;
    }
}