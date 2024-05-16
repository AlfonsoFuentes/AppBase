using Infrastructure.Implementations.Repositories;
using Infrastructure.Implementations.Serialization.JsonConverters;
using Infrastructure.Implementations.Serialization.Serializers;
using Infrastructure.Services.Storage;
using Infrastructure.Services.Storage.Provider;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure.RegisterServices
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddTransient(typeof(IRepositoryAsync<,>), typeof(RepositoryAsync<,>))

                .AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        }

        
        public static IServiceCollection AddServerStorage(this IServiceCollection services)
            => services.AddServerStorage(null!);

        public static IServiceCollection AddServerStorage(this IServiceCollection services, Action<SystemTextJsonOptions> configure)
        {
            return services
                .AddScoped<IJsonSerializer, SystemTextJsonSerializer>()
                .AddScoped<IStorageProvider, ServerStorageProvider>()
                .AddScoped<IServerStorageService, ServerStorageService>()
                .AddScoped<ISyncServerStorageService, ServerStorageService>()
                .Configure<SystemTextJsonOptions>(configureOptions =>
                {
                    configure?.Invoke(configureOptions);
                    if (!configureOptions.JsonSerializerOptions.Converters.Any(c => c.GetType() == typeof(TimespanJsonConverter)))
                        configureOptions.JsonSerializerOptions.Converters.Add(new TimespanJsonConverter());
                });
        }
    }
}