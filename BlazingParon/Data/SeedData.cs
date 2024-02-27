using BlazingParon.Models;
namespace BlazingParon.Data
{
    public static class SeedData
    {
        public static void Initialize(InventoryManagementSystemDB db)
        {
            var products = new Product[]
            {
                new Product { Name = "jTelefon", Price = 8900, Description = "En telefon som är gjord av ett företag som heter Päron" },
                new Product { Name = "jPlatta", Price = 5700, Description = "En platta som är gjord av ett företag som heter Päron"},
                new Product { Name = "Päronklocka", Price = 11000, Description = "En klocka som är gjord av ett företag som heter Päron"}

            };
            db.Products.AddRange(products);
            db.SaveChanges();

            var warehouses = new Warehouse[]
            {
                new Warehouse { Name = "Cupertino" },
                new Warehouse { Name = "Norrköping" },
                new Warehouse { Name = "Frankfurt" }
            };
            db.Warehouses.AddRange(warehouses);
            db.SaveChanges();

            var inventorycounts = new InventoryCount[]
            {
                new InventoryCount { ProductId = 1, WarehouseId = 1, Quantity = 30000 },
                new InventoryCount { ProductId = 2, WarehouseId = 1, Quantity = 20000 },
                new InventoryCount { ProductId = 3, WarehouseId = 1, Quantity = 50000 },
                new InventoryCount { ProductId = 1, WarehouseId = 2, Quantity = 80000 },
                new InventoryCount { ProductId = 2, WarehouseId = 2, Quantity = 10000 },
                new InventoryCount { ProductId = 3, WarehouseId = 2, Quantity = 30000 },
                new InventoryCount { ProductId = 1, WarehouseId = 3, Quantity = 10000 },
                new InventoryCount { ProductId = 2, WarehouseId = 3, Quantity = 90000 },
                new InventoryCount { ProductId = 3, WarehouseId = 3, Quantity = 60000 }
            };
            db.InventoryCounts.AddRange(inventorycounts);
            db.SaveChanges();
        }
    }
}