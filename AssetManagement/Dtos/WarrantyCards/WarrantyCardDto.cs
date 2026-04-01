namespace AssetManagementApi.Dtos.WarrantyCards;

public record WarrantyCardDto(int Id, string Provider, DateTime StartDate, DateTime EndDate, int AssetId);
