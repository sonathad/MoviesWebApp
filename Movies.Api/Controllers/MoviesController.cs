using Microsoft.AspNetCore.Mvc;
using Movies.Api.Mapping;
using Movies.Application.Models;
using Movies.Application.Repositories;
using Movies.Contracts.Requests;

namespace Movies.Api.Controllers;

[ApiController]
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
    [HttpPost(ApiEndpoints.Movies.Create)]
    public async Task<IActionResult> Create(CreateMovieRequest movieRequest)
    {
        var movie = movieRequest.MapToMovie();
        var created = await _movieRepository.CreateAsync(movie);
        if (!created) 
            return BadRequest();
        
        return Created($"{ApiEndpoints.Movies.Create}${movie.Id}", movieRequest);
    }
}