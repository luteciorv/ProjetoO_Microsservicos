namespace Play.Inventory.Service.DTOs
{
    public record ReadInventoryItemDto(Guid ItemId, string Name, string Description, int Amount, DateTimeOffset AcquiredDate);
}
