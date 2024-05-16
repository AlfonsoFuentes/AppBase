using Application.Interfaces.Services.Storage;
using Application.Interfaces.Services.Storage.Provider;
using Asp.Versioning;
using Hangfire;
using Infrastructure.Services.Storage;
using Infrastructure.Services.Storage.Provider;
using Server.Extensions;
using Application.RegisterServices;
using Infrastructure.RegisterServices;
namespace Server
{
    public static class RegisterServices
    {

        public static WebApplicationBuilder AddServerServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddForwarding(builder.Configuration);
            builder.Services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            });
            builder.Services.AddCurrentUserService();
            builder.Services.AddSerialization();
            builder.Services.AddDatabase(builder.Configuration);
            builder.Services.AddServerStorage(); //TODO - should implement ServerStorageProvider to work correctly!
            builder.Services.AddScoped<ServerPreferenceManager>();
            builder.Services.AddServerLocalization();
            builder.Services.AddIdentity();
            builder.Services.AddJwtAuthentication(builder.Services.GetApplicationSettings(builder.Configuration));
            builder.Services.AddSignalR();
            builder.Services.AddApplicationLayer();
            builder.Services.AddApplicationServices();
            builder.Services.AddRepositories();

            builder.Services.AddSharedInfrastructure(builder.Configuration);
            builder.Services.RegisterSwagger();
            builder.Services.AddInfrastructureMappings();
            builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("Default")));
            builder.Services.AddHangfireServer();
            builder.Services.AddControllers().AddValidators();
            //builder.Services.AddExtendedAttributesValidators();
            //builder.Services.AddExtendedAttributesValidators();
            builder.Services.AddRazorPages();
            builder.Services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
            builder.Services.AddLazyCache();
            return builder;
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
