namespace AssetManagementApi.Dtos.Assets;

public record AssetSummaryDto(int Id, string Name, string SerialNumber, int StatusId, string StatusName);
