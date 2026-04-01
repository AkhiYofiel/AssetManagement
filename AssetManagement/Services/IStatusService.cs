using AssetManagementApi.Dtos.Status;
using AssetManagementApi.Services.Results;

namespace AssetManagementApi.Services;

public interface IStatusService
{
    Task<IReadOnlyCollection<StatusDto>> GetStatusesAsync();
    Task<StatusDto?> GetStatusAsync(int id);
    Task<OperationResult<StatusDto>> CreateStatusAsync(StatusCreateUpdateDto dto);
    Task<OperationResult> UpdateStatusAsync(int id, StatusCreateUpdateDto dto);
    Task<OperationResult> DeleteStatusAsync(int id);
}
