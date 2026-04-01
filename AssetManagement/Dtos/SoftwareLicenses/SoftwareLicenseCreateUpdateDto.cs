namespace AssetManagementApi.Dtos.SoftwareLicenses;

public record SoftwareLicenseCreateUpdateDto(string Name, string LicenseKey, DateTime ExpirationDate);
