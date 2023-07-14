using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Play.Commom.Settings;
using Play.Commom.Repositories;
using Play.Common.Repositories.Interfaces;
using Play.Commom.Entities;

namespace Play.Commom.Service.Extensions
{
    public static class MongoDbServiceExtension
    {
        public static void ConfigureMongoDb(this IServiceCollection services)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

            services.AddSingleton(serviceProvider =>
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();

                var mongoDbSettings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
                var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);

                return mongoClient.GetDatabase(serviceSettings.Name);
            });
        }

        public static void ConfigureMongoRepository<TEntity>(this IServiceCollection services, string collectionName) where TEntity : IEntity
        {
            services.AddSingleton<IRepository<TEntity>>(serviceProvider =>
            {
                var database = serviceProvider.GetService<IMongoDatabase>();
                return new MongoRepository<TEntity>(database, collectionName);
            });
        }
    }
}
