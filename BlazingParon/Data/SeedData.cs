using BlazingParon.Models;
namespace BlazingParon.Data
{
    public static class SeedData
    {
        public static void Initialize(InventoryManagementSystemDB db)
        {
            var products = new Product[]
            {
                new Product { Name = "jTelefon", Price = 8900 },
                new Product { Name = "jPlatta", Price = 5700 },
                new Product { Name = "Päronklocka", Price = 11000 }

            };

            var warehouses = new Warehouse[]
            {
                new Warehouse { Name = "Cupertino" },
                new Warehouse { Name = "Norrköping" },
                new Warehouse { Name = "Frankfurt" }
            };
            var inventorycounts = new InventoryCount[]
            {
                new InventoryCount { ProductId = 1, WarehouseId = 1, Quantity = 170000 },
                new InventoryCount { ProductId = 2, WarehouseId = 1, Quantity = 41500 },
                new InventoryCount { ProductId = 3, WarehouseId = 1, Quantity = 90000 },
                new InventoryCount { ProductId = 1, WarehouseId = 2, Quantity = 101700 },
                new InventoryCount { ProductId = 2, WarehouseId = 2, Quantity = 72400 },
                new InventoryCount { ProductId = 3, WarehouseId = 2, Quantity = 25000 },
                new InventoryCount { ProductId = 1, WarehouseId = 3, Quantity = 55000 },
                new InventoryCount { ProductId = 2, WarehouseId = 3, Quantity = 104300 },
                new InventoryCount { ProductId = 3, WarehouseId = 3, Quantity = 38000 }
            };
            db.Products.AddRange(products);
            db.Warehouses.AddRange(warehouses);
            db.InventoryCounts.AddRange(inventorycounts);
            db.SaveChanges();
        }
    }
}