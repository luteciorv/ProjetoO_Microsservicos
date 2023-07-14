using MongoDB.Driver;
using Play.Catalog.Service.Entities;
using Play.Catalog.Service.Repositories.Interfaces;

namespace Play.Catalog.Service.Repositories
{
    public class ItemsRepository : IItemsRepository
    {
        private const string _collectionName = "items";
        private readonly IMongoCollection<Item> _dbCollection;
        private readonly FilterDefinitionBuilder<Item> _filterBuilder;

        public ItemsRepository(IMongoDatabase database)
        {         
            _dbCollection = database.GetCollection<Item>(_collectionName);
            _filterBuilder = Builders<Item>.Filter;
        }

        public async Task<IReadOnlyCollection<Item>> GetAllAsync() =>
            await _dbCollection.Find(_filterBuilder.Empty).ToListAsync();

        public async Task<Item> GetAsync(Guid id)
        {
            var filter = _filterBuilder.Eq(i => i.Id, id);
            return await _dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Item entityToCreate)
        {
            if (entityToCreate is null)
                throw new ArgumentNullException(nameof(entityToCreate));

            await _dbCollection.InsertOneAsync(entityToCreate);
        }

        public async Task UpdateAsync(Item entityToUpdate)
        {
            if (entityToUpdate is null)
                throw new ArgumentNullException(nameof(entityToUpdate));

            var filter = _filterBuilder.Eq(existingEntity => existingEntity.Id, entityToUpdate.Id);
            await _dbCollection.ReplaceOneAsync(filter, entityToUpdate);
        }

        public async Task DeleteAsync(Guid id)
        {
            var filter = _filterBuilder.Eq(existingEntity => existingEntity.Id, id);
            await _dbCollection.DeleteOneAsync(filter);
        }
    }
}
