using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.DTOs;

namespace Play.Catalog.Service.Controllers
{
    [Route("api/itens")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private static readonly List<ReadItemDto> _items = new()
        {
            new ReadItemDto(Guid.NewGuid(), "Poção de vida - P", "Restaura uma quantidade pequena de pontos de vida", 5, DateTimeOffset.UtcNow),
            new ReadItemDto(Guid.NewGuid(), "Antídoto", "Cura o estado de 'Envenenado'", 7, DateTimeOffset.UtcNow),
            new ReadItemDto(Guid.NewGuid(), "Espada de bronze", "Espada banhada de bronze que causa um dano pequeno nos inimigos", 20, DateTimeOffset.UtcNow),
        };

        [HttpGet]
        public ActionResult<IEnumerable<ReadItemDto>> Get() =>
            Ok(_items);

        [HttpGet("{id:guid}")]
        public ActionResult<ReadItemDto> Get([FromRoute] Guid id)
        {
            var item = _items.FirstOrDefault(i => i.Id == id);
            if(item is null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public ActionResult<ReadItemDto> Post([FromBody] CreateItemDto createItemDto)
        {
            var itemDto = new ReadItemDto(Guid.NewGuid(), createItemDto.Name, createItemDto.Description, createItemDto.Price, DateTimeOffset.UtcNow);
            _items.Add(itemDto);

            return CreatedAtAction(nameof(Get), new { itemDto.Id }, itemDto);
        }

        [HttpPut("{id:guid}")]
        public ActionResult<ReadItemDto> Put([FromRoute] Guid id, [FromBody] UpdateItemDto updateItemDto)
        {
            var existingItem = _items.FirstOrDefault(i => i.Id == id);
            if (existingItem is null)
                return NotFound();

            var updatedItem = existingItem with
            {
                Name = updateItemDto.Name,
                Description = updateItemDto.Description,
                Price = updateItemDto.Price
            };

            int index = _items.FindIndex(i => i.Id == id);
            _items[index] = updatedItem;

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public ActionResult Delete(Guid id)
        {
            int index = _items.FindIndex(i => i.Id == id);
            if(index < 0)
                return NotFound();

            _items.RemoveAt(index);

            return NoContent();
        }
    }
}
