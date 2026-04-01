using AssetManagementApi.Dtos.SoftwareLicenses;
using AssetManagementApi.Models;

namespace AssetManagementApi.Mappers;

public interface ISoftwareLicenseMapper
{
    SoftwareLicenseDto ToDto(SoftwareLicense license);
    SoftwareLicenseDetailDto ToDetailDto(SoftwareLicense license);
    SoftwareLicense ToEntity(SoftwareLicenseCreateUpdateDto dto);
    void UpdateEntity(SoftwareLicense license, SoftwareLicenseCreateUpdateDto dto);
}
