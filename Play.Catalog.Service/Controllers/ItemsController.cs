using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.DTOs;
using Play.Catalog.Service.Entities;
using Play.Catalog.Service.Extensions;
using Play.Common.Repositories.Interfaces;

namespace Play.Catalog.Service.Controllers
{
    [Route("api/itens")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IRepository<Item> _itemsRepository;

        public ItemsController(IRepository<Item> itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadItemDto>>> Get()
        {
            var items = await _itemsRepository.GetAllAsync();
            var itemsDto = items.Select(i => i.MapToReadItemDto());
            
            return Ok(itemsDto);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ReadItemDto>> Get([FromRoute] Guid id)
        {
            var item = await _itemsRepository.GetAsync(id);
            if(item is null)
                return NotFound();

            return Ok(item.MapToReadItemDto());
        }

        [HttpPost]
        public async Task<ActionResult<ReadItemDto>> Post([FromBody] CreateItemDto createItemDto)
        {
            var item = createItemDto.MapToItem();

            await _itemsRepository.CreateAsync(item);

            return CreatedAtAction(nameof(Get), new { item.Id }, item);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ReadItemDto>> Put([FromRoute] Guid id, [FromBody] UpdateItemDto updateItemDto)
        {
            var existingItem = await _itemsRepository.GetAsync(id);
            if (existingItem is null)
                return NotFound();

            existingItem.Update(updateItemDto);

            await _itemsRepository.UpdateAsync(existingItem);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var existingItem = await _itemsRepository.GetAsync(id);
            if (existingItem is null)
                return NotFound();

            await _itemsRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
