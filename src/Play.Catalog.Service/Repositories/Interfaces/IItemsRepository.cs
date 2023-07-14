using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service.Repositories.Interfaces
{
    public interface IItemsRepository
    {
        Task CreateAsync(Item entityToCreate);
        Task DeleteAsync(Guid id);
        Task<IReadOnlyCollection<Item>> GetAllAsync();
        Task<Item> GetAsync(Guid id);
        Task UpdateAsync(Item entityToUpdate);
    }
}