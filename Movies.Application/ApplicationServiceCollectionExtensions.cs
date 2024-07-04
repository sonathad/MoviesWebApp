using Microsoft.Extensions.DependencyInjection;
using Movies.Application.Database;
using Movies.Application.Repositories;

namespace Movies.Application;

/// <summary>
///     An extension class that encapsulates the implementation details
///     of the Application layer to simplify API integration with other layers
///     (API, Razor Pages, Blazor, etc.)
/// </summary>
public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IMovieRepository, MovieRepository>();
        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        // NOTE: Singleton because the connection creation is handled by the factory
        services.AddSingleton<IDbConnectionFactory>(_ =>
            new NpgsqlConnectionFactory(connectionString));
        services.AddSingleton<DbInitializer>();
        return services;
    }
}