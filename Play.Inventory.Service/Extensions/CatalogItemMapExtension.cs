using Play.Catalog.Contracts;
using Play.Inventory.Service.Entities;

namespace Play.Inventory.Service.Extensions
{
    public static class CatalogItemMapExtension
    {
        public static CatalogItem MapToCatalogItem(this CatalogItemCreated item) =>
            new(item.ItemId, item.Name, item.Description);

        public static CatalogItem MapToCatalogItem(this CatalogItemUpdated item) =>
            new(item.ItemId, item.Name, item.Description);
    }
}
