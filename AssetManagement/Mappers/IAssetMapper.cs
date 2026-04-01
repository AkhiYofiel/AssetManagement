using AssetManagementApi.Dtos.Assets;
using AssetManagementApi.Models;

namespace AssetManagementApi.Mappers;

public interface IAssetMapper
{
    AssetDto ToDto(Asset asset);
    AssetDetailDto ToDetailDto(Asset asset);
    Asset ToEntity(AssetCreateDto dto);
    void UpdateEntity(Asset asset, AssetUpdateDto dto);
}
