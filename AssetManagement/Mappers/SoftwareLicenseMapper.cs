using AssetManagementApi.Dtos.Assets;
using AssetManagementApi.Dtos.SoftwareLicenses;
using AssetManagementApi.Models;

namespace AssetManagementApi.Mappers;

public class SoftwareLicenseMapper : ISoftwareLicenseMapper
{
    public SoftwareLicenseDto ToDto(SoftwareLicense license)
    {
        return new SoftwareLicenseDto(license.Id, license.Name, license.LicenseKey, license.ExpirationDate);
    }

    public SoftwareLicenseDetailDto ToDetailDto(SoftwareLicense license)
    {
        var assets = license.AssetSoftwareLicenses
            .Select(al => al.Asset)
            .Where(asset => asset is not null)
            .Select(asset => new AssetSummaryDto(
                asset!.Id,
                asset.Name,
                asset.SerialNumber,
                asset.StatusId,
                asset.Status?.Name ?? string.Empty))
            .ToList();

        return new SoftwareLicenseDetailDto(license.Id, license.Name, license.LicenseKey, license.ExpirationDate, assets);
    }

    public SoftwareLicense ToEntity(SoftwareLicenseCreateUpdateDto dto)
    {
        return new SoftwareLicense
        {
            Name = dto.Name,
            LicenseKey = dto.LicenseKey,
            ExpirationDate = dto.ExpirationDate
        };
    }

    public void UpdateEntity(SoftwareLicense license, SoftwareLicenseCreateUpdateDto dto)
    {
        license.Name = dto.Name;
        license.LicenseKey = dto.LicenseKey;
        license.ExpirationDate = dto.ExpirationDate;
    }
}
