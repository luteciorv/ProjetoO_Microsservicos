using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task CreateAsync(TEntity entityToCreate);
        Task DeleteAsync(Guid id);
        Task<IReadOnlyCollection<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(Guid id);
        Task UpdateAsync(TEntity entityToUpdate);
    }
}