using Microsoft.EntityFrameworkCore;
using BlazingParon.Models;
namespace BlazingParon.Data
{
    public class InventoryManagementSystemDB : DbContext
    {
        public InventoryManagementSystemDB(DbContextOptions<InventoryManagementSystemDB> options) : base(options) { }
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Warehouse> Warehouses { get; set; } = null!;
        public DbSet<InventoryCount> InventoryCounts { get; set; }
        public DbSet<Deliveries> Deliveries { get; set; }

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

            modelBuilder.Entity<Deliveries>()
                .HasKey(st => new { st.DeliveryId });

            modelBuilder.Entity<Deliveries>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(st => st.ProductId)
                .OnDelete(DeleteBehavior.Restrict); // Enforce FK to Product

            modelBuilder.Entity<Deliveries>()
                .HasOne<Warehouse>()
                .WithMany()
                .HasForeignKey(st => st.SendingWarehouseId)
                .OnDelete(DeleteBehavior.Restrict); // Enforce FK to Warehouse

            modelBuilder.Entity<Deliveries>()
                .HasOne<Warehouse>()
                .WithMany()
                .HasForeignKey(st => st.ReceivingWarehouseId)
                .OnDelete(DeleteBehavior.Restrict); // Enforce FK to Warehouse
        }
    }
}