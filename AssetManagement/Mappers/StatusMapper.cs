using AssetManagementApi.Dtos.Status;
using AssetManagementApi.Models;

namespace AssetManagementApi.Mappers;

public class StatusMapper : IStatusMapper
{
    public StatusDto ToDto(Status status)
    {
        return new StatusDto(status.Id, status.Name);
    }

    public Status ToEntity(StatusCreateUpdateDto dto)
    {
        return new Status { Name = dto.Name };
    }

    public void UpdateEntity(Status status, StatusCreateUpdateDto dto)
    {
        status.Name = dto.Name;
    }
}
