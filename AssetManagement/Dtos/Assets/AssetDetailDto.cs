using AssetManagementApi.Dtos.SoftwareLicenses;
using AssetManagementApi.Dtos.WarrantyCards;

namespace AssetManagementApi.Dtos.Assets;

public record AssetDetailDto(
    int Id,
    string Name,
    string SerialNumber,
    int StatusId,
    string StatusName,
    int? EmployeeId,
    string? EmployeeName,
    WarrantyCardDto WarrantyCard,
    IReadOnlyCollection<SoftwareLicenseDto> SoftwareLicenses);
