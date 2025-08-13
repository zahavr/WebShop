using WebShop.Shared.Behaviors;

namespace Catalog.API.Configurations;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddExternalServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMapster();
        services.AddCarter(new DependencyContextAssemblyCatalog([typeof(Program).Assembly]));
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        services.AddValidatorsFromAssembly(typeof(Program).Assembly);
        
        services.AddMarten(opt =>
        {
            string? dbConnectionString = configuration.GetConnectionString("Database");
            ArgumentException.ThrowIfNullOrEmpty(dbConnectionString);
            opt.Connection(dbConnectionString);
            }).UseLightweightSessions();

        return services;
    }

    public static IServiceCollection AddInternalServices(this IServiceCollection services)
    {
        return services;
    }
}