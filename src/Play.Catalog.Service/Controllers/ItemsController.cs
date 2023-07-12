using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.DTOs;
using Play.Catalog.Service.Entities;
using Play.Catalog.Service.Extensions;
using Play.Catalog.Service.Repositories;

namespace Play.Catalog.Service.Controllers
{
    [Route("api/itens")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ItemsRepository _repository;

        public ItemsController()
        {
            _repository = new ItemsRepository();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadItemDto>>> Get()
        {
            var items = await _repository.GetAllAsync();
            var itemsDto = items.Select(i => i.MapToReadItemDto());
            
            return Ok(itemsDto);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ReadItemDto>> Get([FromRoute] Guid id)
        {
            var item = await _repository.GetAsync(id);
            if(item is null)
                return NotFound();

            return Ok(item.MapToReadItemDto());
        }

        [HttpPost]
        public async Task<ActionResult<ReadItemDto>> Post([FromBody] CreateItemDto createItemDto)
        {
            var item = createItemDto.MapToItem();

            await _repository.CreateAsync(item);

            return CreatedAtAction(nameof(Get), new { item.Id }, item);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ReadItemDto>> Put([FromRoute] Guid id, [FromBody] UpdateItemDto updateItemDto)
        {
            var existingItem = await _repository.GetAsync(id);
            if (existingItem is null)
                return NotFound();

            existingItem.Update(updateItemDto);

            await _repository.UpdateAsync(existingItem);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var existingItem = await _repository.GetAsync(id);
            if (existingItem is null)
                return NotFound();

            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }
}
