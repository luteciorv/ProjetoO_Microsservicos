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
        private readonly IRepository<InventoryItem> _inventoryRepository;
        private readonly IRepository<CatalogItem> _catalogRepository;

        public InventoryItemsController(IRepository<InventoryItem> repository, IRepository<CatalogItem> catalogRepository)
        {
            _inventoryRepository = repository;
            _catalogRepository = catalogRepository;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AddInventoryItemDto itemDto)
        {
            var item = await _inventoryRepository.GetAsync(i => i.UserId == itemDto.UserId && i.CatalogItemId == itemDto.ItemId);
            if (item is null)
            {
                await _inventoryRepository.CreateAsync(itemDto.MapToItem());
                return Ok();
            }
            
            item.Stack(itemDto.Amount);
            await _inventoryRepository.UpdateAsync(item);

            return NoContent();
        }

        [HttpGet("{userId:guid}")]
        public async Task<ActionResult<IEnumerable<ReadInventoryItemDto>>> Get([FromRoute] Guid userId)
        {
            var inventoryItems = await _inventoryRepository.GetAllAsync(item => item.UserId == userId);
            var inventoryItemsIds = inventoryItems.Select(item => item.CatalogItemId);
            var catalogItems = await _catalogRepository.GetAllAsync(c => inventoryItemsIds.Contains(c.Id));

            var inventoryItemsDto = inventoryItems.Select(inventoryItem =>
            {
                var catalogItemDto = catalogItems.Single(catalogItem => catalogItem.Id == inventoryItem.CatalogItemId);
                return inventoryItem.MapToReadItemDto(catalogItemDto.Name, catalogItemDto.Description);
            });

            return Ok(inventoryItemsDto);
        }
    }
}
