namespace AssetManagementApi.Models;

public class WarrantyCard
{
    public int Id { get; set; }
    public string Provider { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int AssetId { get; set; }
    public Asset? Asset { get; set; }
}
