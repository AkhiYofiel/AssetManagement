using AssetManagementApi.Models;

namespace AssetManagementApi.Repositories;

public interface IAssetRepository : IGenericRepository<Asset>
{
    Task<Asset?> GetDetailAsync(int id);
    Task<Asset?> GetForUpdateAsync(int id);
    Task<Asset?> GetForDeleteAsync(int id);
    Task<bool> StatusExistsAsync(int id);
    Task<bool> EmployeeExistsAsync(int id);
    Task<bool> AssetExistsAsync(int id);
    Task<bool> SoftwareLicenseExistsAsync(int id);
    Task<AssetSoftwareLicense?> FindAssetSoftwareLicenseAsync(int assetId, int licenseId);
    Task AddAssetSoftwareLicenseAsync(AssetSoftwareLicense link);
    Task RemoveAssetSoftwareLicenseAsync(AssetSoftwareLicense link);
}
