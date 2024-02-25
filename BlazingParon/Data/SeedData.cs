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
            db.Products.AddRange(products);
            db.SaveChanges();
        }
    }
}