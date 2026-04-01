using AssetManagementApi.Dtos.Assets;

namespace AssetManagementApi.Dtos.Employees;

public record EmployeeDetailDto(int Id, string FirstName, string LastName, string Email, IReadOnlyCollection<AssetSummaryDto> Assets);
