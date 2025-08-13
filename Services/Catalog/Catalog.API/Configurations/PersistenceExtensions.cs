using Catalog.API.Data;

namespace Catalog.API.Configurations;

internal static class PersistenceExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,
                                                    IConfiguration configuration,
                                                    bool isLocal)
    {
        services.AddMarten(opt =>
        {
            string? dbConnectionString = configuration.GetConnectionString("Database");
            ArgumentException.ThrowIfNullOrEmpty(dbConnectionString);
            opt.Connection(dbConnectionString);
        }).UseLightweightSessions();

        if (isLocal)
        {
            services.InitializeMartenWith<CatalogInitialData>();
        }
        
        return services;
    }
}