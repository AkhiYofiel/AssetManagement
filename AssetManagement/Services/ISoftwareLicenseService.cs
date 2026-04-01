using AssetManagementApi.Dtos.SoftwareLicenses;
using AssetManagementApi.Services.Results;

namespace AssetManagementApi.Services;

public interface ISoftwareLicenseService
{
    Task<IReadOnlyCollection<SoftwareLicenseDto>> GetSoftwareLicensesAsync();
    Task<SoftwareLicenseDetailDto?> GetSoftwareLicenseAsync(int id);
    Task<OperationResult<SoftwareLicenseDto>> CreateSoftwareLicenseAsync(SoftwareLicenseCreateUpdateDto dto);
    Task<OperationResult> UpdateSoftwareLicenseAsync(int id, SoftwareLicenseCreateUpdateDto dto);
    Task<OperationResult> DeleteSoftwareLicenseAsync(int id);
}
