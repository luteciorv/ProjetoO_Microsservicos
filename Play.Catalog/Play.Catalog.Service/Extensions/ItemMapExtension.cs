using Play.Catalog.Contracts.Contracts;
using Play.Catalog.Service.DTOs;
using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service.Extensions
{
    public static class ItemMapExtension
    {
        public static ReadItemDto MapToReadItemDto(this Item entity) =>
            new(entity.Id, entity.Name, entity.Description, entity.Price, entity.CreatedDate);

        public static Item MapToItem(this CreateItemDto createItemDto) =>
            new(createItemDto.Name, createItemDto.Description, createItemDto.Price);

        public static CatalogItemCreated MapToCatalogItemCreated(this Item entity) =>
            new(entity.Id, entity.Name, entity.Description);

        public static CatalogItemUpdated MapToCatalogItemUpdated(this Item entity) =>
           new(entity.Id, entity.Name, entity.Description);

        public static CatalogItemDeleted MapToCatalogItemDeleted(this Item entity) =>
           new(entity.Id);
    }
}
