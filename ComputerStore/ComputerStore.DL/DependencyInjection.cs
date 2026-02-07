using ComputerStore.DL.Interfaces;
using ComputerStore.DL.Repositories;
using ComputerStore.Models.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace ComputerStore.DL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services, IConfiguration configs)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

            // Register your data layer services here
            services
                .AddConfigurations(configs)
                .AddSingleton<IComputerRepository, ComputerMongoRepository>()
                .AddSingleton<ICustomerRepository, CustomerMongoRepository>();

            return services;
        }

        private static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configs)
        {
            // Register your data layer services here
            services
                .Configure<MongoDbConfiguration>(configs.GetSection(nameof(MongoDbConfiguration)));

            return services;
        }
    }
}
