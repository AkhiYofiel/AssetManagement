using AssetManagementApi.Models;

namespace AssetManagementApi.Repositories;

public interface ISoftwareLicenseRepository : IGenericRepository<SoftwareLicense>
{
    Task<SoftwareLicense?> GetDetailAsync(int id);
}