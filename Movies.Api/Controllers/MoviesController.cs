using Microsoft.AspNetCore.Mvc;
using Movies.Application.Models;
using Movies.Application.Repositories;
using Movies.Contracts.Requests;

namespace Movies.Api.Controllers;

[ApiController]
[Route("api")]
public class MoviesController : ControllerBase
{
    private readonly IMovieRepository _movieRepository;

    public MoviesController(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    /// <summary>
    /// Creates a new movie.
    /// </summary>
    /// <param name="movieRequest">The movie to be created.</param>
    /// <returns>A new created movie.</returns>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("movies")]
    public async Task<IActionResult> Create(CreateMovieRequest movieRequest)
    {
        var movieId = Guid.NewGuid();
        var movie = new Movie
        {
            Id = movieId,
            Title = movieRequest.Title,
            YearOfRelease = movieRequest.YearOfRelease,
            Genres = movieRequest.Genres.ToList()
        };
        
        var created = await _movieRepository.CreateAsync(movie);
        if (!created) 
            return BadRequest();
        
        return Created($"/api/movies/${movieId}", movieRequest);
    }
}