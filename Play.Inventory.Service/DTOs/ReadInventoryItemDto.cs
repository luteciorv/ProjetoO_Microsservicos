namespace Play.Inventory.Service.DTOs
{
    public record ReadInventoryItemDto(Guid ItemId, int Amount, DateTimeOffset AcquiredDate);
}
