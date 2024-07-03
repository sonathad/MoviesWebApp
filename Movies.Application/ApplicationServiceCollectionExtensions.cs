using Microsoft.Extensions.DependencyInjection;
using Movies.Application.Repositories;

namespace Movies.Application;

/// <summary>
/// An extension class that encapsulates the implementation details
/// of the Application layer, for ease of use by multiple consumers
/// (API, Razor Pages, Blazor, etc.)
/// </summary>
public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IMovieRepository, MovieRepository>();
        return services;
    }
}
