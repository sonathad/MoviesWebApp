using Movies.Application.Models;
using Movies.Contracts.Requests;

namespace Movies.Api.Mapping;

public static class ContractMapping
{
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
}