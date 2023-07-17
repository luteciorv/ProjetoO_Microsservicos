using MassTransit;
using MassTransit.Testing;
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
        private readonly IPublishEndpoint _publishEndpoint;

        public ItemsController(IRepository<Item> itemsRepository, IPublishEndpoint publishEndpoint)
        {
            _itemsRepository = itemsRepository;
            _publishEndpoint = publishEndpoint;
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
            if(item is null)  return NotFound();

            return Ok(item.MapToReadItemDto());
        }

        [HttpPost]
        public async Task<ActionResult<ReadItemDto>> Post([FromBody] CreateItemDto createItemDto)
        {
            var item = createItemDto.MapToItem();

            await _itemsRepository.CreateAsync(item);

            await _publishEndpoint.Publish(item.MapToCatalogItemCreated());

            return CreatedAtAction(nameof(Get), new { item.Id }, item);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ReadItemDto>> Put([FromRoute] Guid id, [FromBody] UpdateItemDto updateItemDto)
        {
            var item = await _itemsRepository.GetAsync(id);
            if (item is null) return NotFound();

            item.Update(updateItemDto);
            await _itemsRepository.UpdateAsync(item);

            await _publishEndpoint.Publish(item.MapToCatalogItemUpdated());

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var item = await _itemsRepository.GetAsync(id);
            if (item is null) return NotFound();
                
            await _itemsRepository.DeleteAsync(id);

            await _publishEndpoint.Publish(item.MapToCatalogItemDeleted());

            return NoContent();
        }
    }
}
