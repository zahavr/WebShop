namespace Catalog.API.Configurations;

internal static class HealthChecksExtensions
{
    public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
                .AddNpgSql(configuration.GetConnectionString("Database") ?? throw new NullReferenceException("Database"));
        
        return services;
    }
}