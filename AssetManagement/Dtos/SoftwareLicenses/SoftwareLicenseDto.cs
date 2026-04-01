namespace AssetManagementApi.Dtos.SoftwareLicenses;

public record SoftwareLicenseDto(int Id, string Name, string LicenseKey, DateTime ExpirationDate);
