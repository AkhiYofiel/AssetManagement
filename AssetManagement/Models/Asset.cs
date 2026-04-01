namespace AssetManagementApi.Models;

public class Asset
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public int StatusId { get; set; }
    public Status? Status { get; set; }
    public int? EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public WarrantyCard? WarrantyCard { get; set; }
    public ICollection<AssetSoftwareLicense> AssetSoftwareLicenses { get; set; } = new List<AssetSoftwareLicense>();
}
