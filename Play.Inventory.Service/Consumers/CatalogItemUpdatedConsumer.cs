using MassTransit;
using Play.Catalog.Contracts;
using Play.Common.Repositories.Interfaces;
using Play.Inventory.Service.Entities;
using Play.Inventory.Service.Extensions;

namespace Play.Inventory.Service.Consumers
{
    public class CatalogItemUpdatedConsumer : IConsumer<CatalogItemUpdated>
    {
        private readonly IRepository<CatalogItem> _repository;

        public CatalogItemUpdatedConsumer(IRepository<CatalogItem> repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<CatalogItemUpdated> context)
        {
            var message = context.Message;

            var item = await _repository.GetAsync(message.ItemId);
            if (item is null)
            {
                var catalogItem = message.MapToCatalogItem();
                await _repository.CreateAsync(catalogItem);
            }
            else
            {
                item.Update(message.Name, message.Description);
                await _repository.UpdateAsync(item);
            }
        }
    }
}
