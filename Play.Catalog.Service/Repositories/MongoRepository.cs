using MongoDB.Driver;
using Play.Catalog.Service.Entities;
using Play.Catalog.Service.Repositories.Interfaces;

namespace Play.Catalog.Service.Repositories
{
    public class MongoRepository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        private readonly IMongoCollection<TEntity> _dbCollection;
        private readonly FilterDefinitionBuilder<TEntity> _filterBuilder;

        public MongoRepository(IMongoDatabase database, string collectionName)
        {         
            _dbCollection = database.GetCollection<TEntity>(collectionName);
            _filterBuilder = Builders<TEntity>.Filter;
        }

        public async Task<IReadOnlyCollection<TEntity>> GetAllAsync() =>
            await _dbCollection.Find(_filterBuilder.Empty).ToListAsync();

        public async Task<TEntity> GetAsync(Guid id)
        {
            var filter = _filterBuilder.Eq(i => i.Id, id);
            return await _dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(TEntity entityToCreate)
        {
            if (entityToCreate is null)
                throw new ArgumentNullException(nameof(entityToCreate));

            await _dbCollection.InsertOneAsync(entityToCreate);
        }

        public async Task UpdateAsync(TEntity entityToUpdate)
        {
            if (entityToUpdate is null)
                throw new ArgumentNullException(nameof(entityToUpdate));

            var filter = _filterBuilder.Eq(existingEntity => existingEntity.Id, entityToUpdate.Id);
            await _dbCollection.ReplaceOneAsync(filter, entityToUpdate);
        }

        public async Task DeleteAsync(Guid id)
        {
            var filter = _filterBuilder.Eq(existingEntity => existingEntity.Id, id);
            DeleteResult deleteResult = await _dbCollection.DeleteOneAsync(filter);
        }
    }
}
