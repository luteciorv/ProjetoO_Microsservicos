using MassTransit;
using Play.Catalog.Contracts;
using Play.Common.Repositories.Interfaces;
using Play.Inventory.Service.Entities;
using Play.Inventory.Service.Extensions;

namespace Play.Inventory.Service.Consumers
{
    public class CatalogItemCreatedConsumer : IConsumer<CatalogItemCreated>
    {
        private readonly IRepository<CatalogItem> _repository;

        public CatalogItemCreatedConsumer(IRepository<CatalogItem> repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<CatalogItemCreated> context)
        {
            var message = context.Message;

            var item = await _repository.GetAsync(message.ItemId);
            if (item is not null) return;

            var catalogItem = message.MapToCatalogItem();

            await _repository.CreateAsync(catalogItem);
        }
    }
}
