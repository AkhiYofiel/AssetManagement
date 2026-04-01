using Microsoft.EntityFrameworkCore;
using AssetManagementApi.Data;
using AssetManagementApi.Models;

namespace AssetManagementApi.Repositories;

public class SoftwareLicenseRepository : GenericRepository<SoftwareLicense>, ISoftwareLicenseRepository
{
    private readonly AssetManagementContext _assetContext;

    public SoftwareLicenseRepository(AssetManagementContext context) : base(context)
    {
        _assetContext = context;
    }

    public Task<SoftwareLicense?> GetDetailAsync(int id)
    {
        return _assetContext.SoftwareLicenses
            .AsNoTracking()
            .Include(l => l.AssetSoftwareLicenses)
            .ThenInclude(al => al.Asset)
            .ThenInclude(a => a.Status)
            .FirstOrDefaultAsync(l => l.Id == id);
    }
}
