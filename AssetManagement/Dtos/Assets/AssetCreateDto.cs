using AssetManagementApi.Dtos.WarrantyCards;

namespace AssetManagementApi.Dtos.Assets;

public record AssetCreateDto(
    string Name,
    string SerialNumber,
    int StatusId,
    int? EmployeeId,
    WarrantyCardCreateDto WarrantyCard);
