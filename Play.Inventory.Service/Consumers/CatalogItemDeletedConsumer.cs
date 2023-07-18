﻿using MassTransit;
using Play.Catalog.Contracts;
using Play.Common.Repositories.Interfaces;
using Play.Inventory.Service.Entities;
using Play.Inventory.Service.Extensions;

namespace Play.Inventory.Service.Consumers
{
    public class CatalogItemDeletedConsumer : IConsumer<CatalogItemDeleted>
    {
        private readonly IRepository<CatalogItem> _repository;

        public CatalogItemDeletedConsumer(IRepository<CatalogItem> repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<CatalogItemDeleted> context)
        {
            var message = context.Message;

            var item = await _repository.GetAsync(message.ItemId);
            if (item is null) return;
            
            await _repository.DeleteAsync(message.ItemId);
        }
    }
}
