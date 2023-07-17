using Microsoft.AspNetCore.Mvc;
using Play.Common.Repositories.Interfaces;
using Play.Inventory.Service.Clients;
using Play.Inventory.Service.DTOs;
using Play.Inventory.Service.Entities;
using Play.Inventory.Service.Extensions;

namespace Play.Inventory.Service.Controllers
{
    [Route("api/itens-inventario")]
    [ApiController]
    public class InventoryItemsController : ControllerBase
    {
        private readonly IRepository<InventoryItem> _repository;
        private readonly CatalogClient _catalogClient;

        public InventoryItemsController(IRepository<InventoryItem> repository, CatalogClient catalogClient)
        {
            _repository = repository;
            _catalogClient = catalogClient;
        }

        [HttpPost]
        public async Task<ActionResult<ReadInventoryItemDto>> Post([FromBody] AddInventoryItemDto itemDto)
        {
            var item = await _repository.GetAsync(i => i.UserId == itemDto.UserId && i.CatalogItemId == itemDto.ItemId);
            if (item is null)
            {
                await _repository.CreateAsync(itemDto.MapToItem());

                var catalogItems = await _catalogClient.GetCatalogItemsAsync();
                if (catalogItems is null)
                    return NotFound();

                var catalogItemDto = catalogItems.Single(catalogItem => catalogItem.Id == itemDto.ItemId);
                var inventoryDto = itemDto.MapToItem().MapToReadItemDto(catalogItemDto.Name, catalogItemDto.Description);

                return CreatedAtAction(nameof(Get), new { itemDto.UserId }, inventoryDto);
            }
            
            item.Stack(itemDto.Amount);
            await _repository.UpdateAsync(item);

            return NoContent();
        }

        [HttpGet("{userId:guid}")]
        public async Task<ActionResult<IEnumerable<ReadInventoryItemDto>>> Get([FromRoute] Guid userId)
        {
            var catalogItems = await _catalogClient.GetCatalogItemsAsync();
            if (catalogItems is null) 
                return NotFound();

            var inventoryItems = await _repository.GetAllAsync(item => item.UserId == userId);

            var inventoryItemsDto = inventoryItems.Select(inventoryItem =>
            {
                var catalogItemDto = catalogItems.Single(catalogItem => catalogItem.Id == inventoryItem.CatalogItemId);
                return inventoryItem.MapToReadItemDto(catalogItemDto.Name, catalogItemDto.Description);
            });

            return Ok(inventoryItemsDto);
        }
    }
}
