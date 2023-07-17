using Microsoft.AspNetCore.Mvc;
using Play.Common.Repositories.Interfaces;
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

        public InventoryItemsController(IRepository<InventoryItem> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<ActionResult<ReadInventoryItemDto>> Post([FromBody] AddInventoryItemDto itemDto)
        {
            var item = await _repository.GetAsync(i => i.UserId == itemDto.UserId && i.ItemId == itemDto.ItemId);
            if (item is null)
            {
                await _repository.CreateAsync(itemDto.MapToItem());
                return CreatedAtAction(nameof(Get), new { itemDto.UserId }, itemDto.MapToItem().MapToReadItemDto());
            }
            
            item.Stack(item.Amount);
            await _repository.UpdateAsync(item);

            return NoContent();
        }

        [HttpGet("{userId:guid}")]
        public async Task<ActionResult<IEnumerable<ReadInventoryItemDto>>> Get([FromRoute] Guid userId)
        {
            var items = await _repository.GetAllAsync(item => item.UserId == userId);
            var itemsDto = items.Select(i => i.MapToReadItemDto());

            return Ok(itemsDto);
        }
    }
}
