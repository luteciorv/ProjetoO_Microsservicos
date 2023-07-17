﻿using Play.Commom.Entities;
using System.Linq.Expressions;

namespace Play.Common.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task CreateAsync(TEntity entityToCreate);
        Task DeleteAsync(Guid id);

        Task<IReadOnlyCollection<TEntity>> GetAllAsync();
        Task<IReadOnlyCollection<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter);

        Task<TEntity> GetAsync(Guid id);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter);

        Task UpdateAsync(TEntity entityToUpdate);
    }
}