using Microsoft.EntityFrameworkCore;
using AssetManagementApi.Data;
using AssetManagementApi.Models;

namespace AssetManagementApi.Repositories;

public class AssetRepository : GenericRepository<Asset>, IAssetRepository
{
    private AssetManagementContext Context => (AssetManagementContext)_context;

    public AssetRepository(AssetManagementContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<Asset>> GetAllAsync()
    {
        return await Context.Assets
            .AsNoTracking()
            .Include(a => a.Status)
            .Include(a => a.Employee)
            .ToListAsync();
    }

    public Task<Asset?> GetDetailAsync(int id)
    {
        return Context.Assets
            .AsNoTracking()
            .Include(a => a.Status)
            .Include(a => a.Employee)
            .Include(a => a.WarrantyCard)
            .Include(a => a.AssetSoftwareLicenses)
            .ThenInclude(al => al.SoftwareLicense)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public Task<Asset?> GetForUpdateAsync(int id)
    {
        return Context.Assets
            .Include(a => a.WarrantyCard)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public Task<Asset?> GetForDeleteAsync(int id)
    {
        return Context.Assets
            .Include(a => a.WarrantyCard)
            .Include(a => a.AssetSoftwareLicenses)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public Task<bool> StatusExistsAsync(int id)
    {
        return Context.Statuses.AnyAsync(s => s.Id == id);
    }

    public Task<bool> EmployeeExistsAsync(int id)
    {
        return Context.Employees.AnyAsync(e => e.Id == id);
    }

    public Task<bool> AssetExistsAsync(int id)
    {
        return Context.Assets.AnyAsync(a => a.Id == id);
    }

    public Task<bool> SoftwareLicenseExistsAsync(int id)
    {
        return Context.SoftwareLicenses.AnyAsync(l => l.Id == id);
    }

    public Task<AssetSoftwareLicense?> FindAssetSoftwareLicenseAsync(int assetId, int licenseId)
    {
        return Context.AssetSoftwareLicenses.FindAsync(assetId, licenseId).AsTask();
    }

    public Task AddAssetSoftwareLicenseAsync(AssetSoftwareLicense link)
    {
        Context.AssetSoftwareLicenses.Add(link);
        return Task.CompletedTask;
    }

    public Task RemoveAssetSoftwareLicenseAsync(AssetSoftwareLicense link)
    {
        Context.AssetSoftwareLicenses.Remove(link);
        return Task.CompletedTask;
    }

}
