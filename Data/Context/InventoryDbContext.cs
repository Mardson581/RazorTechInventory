using TechInventory.Models;
using Microsoft.EntityFrameworkCore;

namespace TechInventory.Data.Context;

public class InventoryDbContext : DbContext
{
    public DbSet<Device> Devices { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<DeviceModel> DeviceModels { get; set; }
    public DbSet<MaintenanceRecord> MaintenanceRecords { get; set; }


    public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DeviceModel>()
            .HasOne(dm => dm.Brand)
            .WithMany()
            .HasForeignKey(dm => dm.BrandId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Device>()
            .HasOne(d => d.Model)
            .WithMany()
            .HasForeignKey(d => d.DeviceModelId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}