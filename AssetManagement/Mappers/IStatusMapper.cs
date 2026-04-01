using AssetManagementApi.Dtos.Status;
using AssetManagementApi.Models;

namespace AssetManagementApi.Mappers;

public interface IStatusMapper
{
    StatusDto ToDto(Status status);
    Status ToEntity(StatusCreateUpdateDto dto);
    void UpdateEntity(Status status, StatusCreateUpdateDto dto);
}
