namespace AssetManagementApi.Models;

public class SoftwareLicense
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LicenseKey { get; set; } = string.Empty;
    public DateTime ExpirationDate { get; set; }
    public ICollection<AssetSoftwareLicense> AssetSoftwareLicenses { get; set; } = new List<AssetSoftwareLicense>();
}
