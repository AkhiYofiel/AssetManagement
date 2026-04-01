namespace AssetManagementApi.Models;

public class AssetSoftwareLicense
{
    public int AssetId { get; set; }
    public Asset? Asset { get; set; }
    public int SoftwareLicenseId { get; set; }
    public SoftwareLicense? SoftwareLicense { get; set; }
}
