using AssetManagementApi.Dtos.WarrantyCards;

namespace AssetManagementApi.Dtos.Assets;

public record AssetUpdateDto(
    string Name,
    string SerialNumber,
    int StatusId,
    int? EmployeeId,
    WarrantyCardUpdateDto WarrantyCard);
