using AssetManagementApi.Dtos.Assets;
using AssetManagementApi.Dtos.SoftwareLicenses;
using AssetManagementApi.Dtos.WarrantyCards;
using AssetManagementApi.Models;

namespace AssetManagementApi.Mappers;

public class AssetMapper : IAssetMapper
{
    public AssetDto ToDto(Asset asset)
    {
        var employeeName = asset.Employee is null ? null : $"{asset.Employee.FirstName} {asset.Employee.LastName}";

        return new AssetDto(
            asset.Id,
            asset.Name,
            asset.SerialNumber,
            asset.StatusId,
            asset.Status?.Name ?? string.Empty,
            asset.EmployeeId,
            employeeName);
    }

    public AssetDetailDto ToDetailDto(Asset asset)
    {
        var employeeName = asset.Employee is null ? null : $"{asset.Employee.FirstName} {asset.Employee.LastName}";

        var warranty = new WarrantyCardDto(
            asset.WarrantyCard!.Id,
            asset.WarrantyCard.Provider,
            asset.WarrantyCard.StartDate,
            asset.WarrantyCard.EndDate,
            asset.Id);

        var licenses = asset.AssetSoftwareLicenses
            .Select(al => al.SoftwareLicense)
            .Where(sl => sl is not null)
            .Select(sl => new SoftwareLicenseDto(sl!.Id, sl.Name, sl.LicenseKey, sl.ExpirationDate))
            .ToList();

        return new AssetDetailDto(
            asset.Id,
            asset.Name,
            asset.SerialNumber,
            asset.StatusId,
            asset.Status?.Name ?? string.Empty,
            asset.EmployeeId,
            employeeName,
            warranty,
            licenses);
    }

    public Asset ToEntity(AssetCreateDto dto)
    {
        return new Asset
        {
            Name = dto.Name,
            SerialNumber = dto.SerialNumber,
            StatusId = dto.StatusId,
            EmployeeId = dto.EmployeeId,
            WarrantyCard = new WarrantyCard
            {
                Provider = dto.WarrantyCard.Provider,
                StartDate = dto.WarrantyCard.StartDate,
                EndDate = dto.WarrantyCard.EndDate
            }
        };
    }

    public void UpdateEntity(Asset asset, AssetUpdateDto dto)
    {
        asset.Name = dto.Name;
        asset.SerialNumber = dto.SerialNumber;
        asset.StatusId = dto.StatusId;
        asset.EmployeeId = dto.EmployeeId;
        asset.WarrantyCard!.Provider = dto.WarrantyCard.Provider;
        asset.WarrantyCard.StartDate = dto.WarrantyCard.StartDate;
        asset.WarrantyCard.EndDate = dto.WarrantyCard.EndDate;
    }
}
