using Microsoft.EntityFrameworkCore;
using AssetManagementApi.Models;
namespace AssetManagementApi.Data
{
    public class AssetManagementContext : DbContext
    {
        public AssetManagementContext(DbContextOptions<AssetManagementContext> options) : base(options)
        {
        }
        public DbSet<Asset> Assets => Set<Asset>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<SoftwareLicense> SoftwareLicenses => Set<SoftwareLicense>();
        public DbSet<AssetSoftwareLicense> AssetSoftwareLicenses => Set<AssetSoftwareLicense>();
        public DbSet<Status> Statuses => Set<Status>();
        public DbSet<WarrantyCard> WarrantyCards => Set<WarrantyCard>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>(entity =>
            {
                entity.HasOne(a => a.WarrantyCard).WithOne(w => w.Asset).HasForeignKey<WarrantyCard>(w => w.AssetId).IsRequired();

                entity.HasOne(a => a.Employee).WithMany(e => e.Assets).HasForeignKey(a => a.EmployeeId);

                entity.HasOne(a => a.Status).WithMany(s => s.Assets).HasForeignKey(a => a.StatusId).IsRequired();
            });

            modelBuilder.Entity<AssetSoftwareLicense>(entity =>
            {
                entity.HasKey(x => new { x.AssetId, x.SoftwareLicenseId });

                entity.HasOne(x => x.Asset).WithMany(a => a.AssetSoftwareLicenses).HasForeignKey(x => x.AssetId);

                entity.HasOne(x => x.SoftwareLicense).WithMany(s => s.AssetSoftwareLicenses).HasForeignKey(x => x.SoftwareLicenseId);
            });

            modelBuilder.Entity<WarrantyCard>(entity =>
            {
                entity.HasIndex(w => w.AssetId).IsUnique();
            });

        }
    }
}