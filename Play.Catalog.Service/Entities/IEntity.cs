namespace Play.Catalog.Service.Entities
{
    public interface IEntity
    {
        Guid Id { get; protected set; }
        DateTimeOffset CreatedDate { get; protected set; }
    }
}
