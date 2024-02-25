using Microsoft.EntityFrameworkCore;
using BlazingParon.Models;
namespace BlazingParon.Data
{
    class InventoryManagementSystemDB : DbContext
    {
        public InventoryManagementSystemDB(DbContextOptions<InventoryManagementSystemDB> options) : base(options) { }
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Warehouse> Warehouses { get; set; } = null!;
        public DbSet<InventoryCount> InventoryCounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InventoryCount>()
                .HasKey(ic => new { ic.ProductId, ic.WarehouseId }); // Composite key

            modelBuilder.Entity<InventoryCount>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(ic => ic.ProductId)
                .OnDelete(DeleteBehavior.Restrict); // Enforce FK to Product

            modelBuilder.Entity<InventoryCount>()
                .HasOne<Warehouse>()
                .WithMany()
                .HasForeignKey(ic => ic.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict); // Enforce FK to Warehouse
        }
    }
}