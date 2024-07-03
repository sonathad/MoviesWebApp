using Microsoft.AspNetCore.Mvc;
using Movies.Api.Mapping;
using Movies.Application.Repositories;
using Movies.Contracts.Requests;

namespace Movies.Api.Controllers;

/// <summary>
/// The controller that corresponds to the endpoints that CRUD movie resources.
/// </summary>
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
    /// <response code="201">The movie was added successfully.</response>
    /// <response code="400">The request was invalid.</response>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost(ApiEndpoints.Movies.Create)]
    public async Task<IActionResult> Create(CreateMovieRequest movieRequest)
    {
        var movie = movieRequest.MapToMovie();
        var created = await _movieRepository.CreateAsync(movie);
        if (!created) 
            return BadRequest();

        var movieResponse = movie.MapToResponse();
        return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie);
    }

    /// <summary>
    /// Retrieves a specific movie by id.
    /// </summary>
    /// <response code="200">Gets the movie that corresponds to the supplied id.</response>
    /// <response code="404">A movie with the requested id was not found.</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet(ApiEndpoints.Movies.Get)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var movie = await _movieRepository.GetByIdAsync(id);
        if (movie is null)
        {
            return NotFound();
        }

        var response = movie.MapToResponse();
        return Ok(response);
    }

    /// <summary>
    /// Fetch all movies from the database.
    /// </summary>
    /// <response code="200">All movies from the database.</response>
    [HttpGet(ApiEndpoints.Movies.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var movies = await _movieRepository.GetAllAsync();
        var moviesResponse = movies.MapToResponse();
        return Ok(moviesResponse);
    }
}