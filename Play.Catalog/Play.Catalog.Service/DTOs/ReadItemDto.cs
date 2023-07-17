namespace Play.Catalog.Service.DTOs
{
    public record ReadItemDto(Guid Id, string Name, string Description, decimal Price, DateTimeOffset CreatedDate);
}
