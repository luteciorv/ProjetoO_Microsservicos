namespace Play.Inventory.Service.DTOs
{
    public record AddInventoryItemDto(Guid UserId, Guid ItemId, int Amount);
}
