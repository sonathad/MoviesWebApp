using Movies.Application.Models;
using Movies.Contracts.Requests;
using Movies.Contracts.Responses;

namespace Movies.Api.Mapping;

/// <summary>
///     A mapper class to be used instead of adding a NuGet package
///     that handles transformation between domain objects
/// </summary>
public static class ContractMapping
{
    /// <summary>
    ///     A method that converts a CreateMovieRequest to a Movie domain entity.
    /// </summary>
    /// <returns>A new Movie entity.</returns>
    public static Movie MapToMovie(this CreateMovieRequest movieRequest)
    {
        return new Movie
        {
            Id = Guid.NewGuid(),
            Title = movieRequest.Title,
            YearOfRelease = movieRequest.YearOfRelease,
            Genres = movieRequest.Genres.ToList()
        };
    }

    /// <summary>
    ///     A method that converts an UpdateMovieRequest to a Movie domain entity.
    /// </summary>
    /// <returns>A new Movie entity.</returns>
    public static Movie MapToMovie(this UpdateMovieRequest movieRequest, Guid id)
    {
        return new Movie
        {
            Id = id,
            Title = movieRequest.Title,
            YearOfRelease = movieRequest.YearOfRelease,
            Genres = movieRequest.Genres.ToList()
        };
    }

    /// <summary>
    ///     A Movie entity to a Movie response mapper.
    /// </summary>
    /// <param name="movie">A movie Entity.</param>
    /// <returns></returns>
    public static MovieResponse MapToResponse(this Movie movie)
    {
        return new MovieResponse
        {
            Id = movie.Id,
            Title = movie.Title,
            Slug = movie.Slug,
            YearOfRelease = movie.YearOfRelease,
            Genres = movie.Genres
        };
    }

    /// <summary>
    ///     Converts a collection of movie entities to a Movies response.
    /// </summary>
    /// <param name="movies">A collection of movie entities.</param>
    /// <returns>An object containing a collection of movie responses.</returns>
    public static MoviesResponse MapToResponse(this IEnumerable<Movie> movies)
    {
        return new MoviesResponse
        {
            Items = movies.Select(MapToResponse)
        };
    }
}