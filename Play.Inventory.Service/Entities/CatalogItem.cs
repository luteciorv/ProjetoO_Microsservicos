using Play.Commom.Entities;

namespace Play.Inventory.Service.Entities
{
    public class CatalogItem : IEntity
    {
        public CatalogItem(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public Guid Id { get; set; }
        public DateTimeOffset CreatedDate { get; set;}

        public string Name { get; private set; }
        public string Description { get; private set; } 

        public void Update(string name, string description) => 
            (Name, Description) = (name, description);
    }
}
