namespace Catalog.API.Configurations;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddExternalServices(this IServiceCollection services)
    {
        services.AddMapster();
        services.AddCarter(new DependencyContextAssemblyCatalog([typeof(Program).Assembly]));
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });

        return services;
    }

    public static IServiceCollection AddInternalServices(this IServiceCollection services)
    {
        return services;
    }
}