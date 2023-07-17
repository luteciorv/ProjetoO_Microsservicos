using Play.Catalog.Service.DTOs;
using Play.Commom.Entities;

namespace Play.Catalog.Service.Entities
{
    public class Item : IEntity
    {
        public Item(string name, string description, decimal price)
        {
            Id = Guid.NewGuid();

            Name = name;
            Description = description;
            Price = price;

            CreatedDate = DateTime.UtcNow;
        }

        public Guid Id { get; set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public DateTimeOffset CreatedDate { get; set; }

        public void Update(UpdateItemDto updateItemDto)
        {
            Name = updateItemDto.Name;
            Description = updateItemDto.Description;
            Price = updateItemDto.Price;
        }
    }
}
