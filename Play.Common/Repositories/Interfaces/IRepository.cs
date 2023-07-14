using Play.Commom.Entities;

namespace Play.Common.Repositories.Interfaces
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