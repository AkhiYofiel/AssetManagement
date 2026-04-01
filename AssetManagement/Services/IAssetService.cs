using AssetManagementApi.Dtos.Assets;
using AssetManagementApi.Services.Results;

namespace AssetManagementApi.Services;

public interface IAssetService
{
    Task<IReadOnlyCollection<AssetDto>> GetAssetsAsync();
    Task<AssetDetailDto?> GetAssetAsync(int id);
    Task<OperationResult<AssetDetailDto>> CreateAssetAsync(AssetCreateDto dto);
    Task<OperationResult> UpdateAssetAsync(int id, AssetUpdateDto dto);
    Task<OperationResult> DeleteAssetAsync(int id);
    Task<OperationResult> AssignLicenseAsync(int assetId, int licenseId);
    Task<OperationResult> RemoveLicenseAsync(int assetId, int licenseId);
}
