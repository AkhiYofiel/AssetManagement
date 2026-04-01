using AssetManagementApi.Dtos.Assets;

namespace AssetManagementApi.Dtos.SoftwareLicenses;

public record SoftwareLicenseDetailDto(int Id, string Name, string LicenseKey, DateTime ExpirationDate, IReadOnlyCollection<AssetSummaryDto> Assets);
