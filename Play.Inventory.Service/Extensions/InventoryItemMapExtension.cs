using Play.Inventory.Service.DTOs;
using Play.Inventory.Service.Entities;

namespace Play.Inventory.Service.Extensions
{
    public static class InventoryItemMapExtension
    {
        public static ReadInventoryItemDto MapToReadItemDto(this InventoryItem entity, string name, string description) =>
            new(entity.CatalogItemId, name, description, entity.Amount, entity.AcquiredDate);

        public static InventoryItem MapToItem(this AddInventoryItemDto dto) =>
            new(dto.UserId, dto.ItemId, dto.Amount);
    }
}
