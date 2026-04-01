namespace AssetManagementApi.Dtos.Assets;

public record AssetDto(
    int Id,
    string Name,
    string SerialNumber,
    int StatusId,
    string StatusName,
    int? EmployeeId,
    string? EmployeeName);
