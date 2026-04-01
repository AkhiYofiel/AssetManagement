using FluentValidation;
using AssetManagementApi.Dtos.Status;
using AssetManagementApi.Mappers;
using AssetManagementApi.Models;
using AssetManagementApi.Repositories;
using AssetManagementApi.Services.Results;

namespace AssetManagementApi.Services;

public class StatusService : GenericService<Status, StatusDto, StatusCreateUpdateDto>, IStatusService
{
    private readonly IStatusMapper _statusMapper;

    public StatusService(
        IStatusRepository statusRepository,
        IStatusMapper statusMapper,
        IValidator<StatusCreateUpdateDto> validator)
        : base(statusRepository, validator)
    {
        _statusMapper = statusMapper;
    }

    public async Task<IReadOnlyCollection<StatusDto>> GetStatusesAsync()
    {
        return await GetAllAsync();
    }

    public async Task<StatusDto?> GetStatusAsync(int id)
    {
        return await GetByIdAsync(id);
    }

    public async Task<OperationResult<StatusDto>> CreateStatusAsync(StatusCreateUpdateDto dto)
    {
        return await CreateAsync(dto);
    }

    public async Task<OperationResult> UpdateStatusAsync(int id, StatusCreateUpdateDto dto)
    {
        return await UpdateAsync(id, dto);
    }

    public async Task<OperationResult> DeleteStatusAsync(int id)
    {
        return await DeleteAsync(id);
    }

    protected override StatusDto MapToDto(Status entity) => _statusMapper.ToDto(entity);

    protected override Status MapToEntity(StatusCreateUpdateDto dto) => _statusMapper.ToEntity(dto);

    protected override void UpdateEntity(Status entity, StatusCreateUpdateDto dto) => _statusMapper.UpdateEntity(entity, dto);
}
