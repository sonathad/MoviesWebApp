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
        return CreatedAtAction(nameof(Get), new { idOrSlug = movie.Id }, movie);
    }

    /// <summary>
    /// Retrieves a specific movie by id.
    /// </summary>
    /// <response code="200">Gets the movie that corresponds to the provided id.</response>
    /// <response code="404">A movie with the requested id was not found.</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet(ApiEndpoints.Movies.Get)]
    public async Task<IActionResult> Get([FromRoute] string idOrSlug)
    {
        var movie = Guid.TryParse(idOrSlug, out var id)
            ? await _movieRepository.GetByIdAsync(id)
            : await _movieRepository.GetBySlugAsync(idOrSlug);
        if (movie is null)
        {
            return NotFound();
        }

        var response = movie.MapToResponse();
        return Ok(response);
    }

    /// <summary>
    /// Get all movies from the database.
    /// </summary>
    /// <response code="200">The movies that exist in the database.</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet(ApiEndpoints.Movies.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var movies = await _movieRepository.GetAllAsync();
        var moviesResponse = movies.MapToResponse();
        return Ok(moviesResponse);
    }

    /// <summary>
    /// Update the fields of the movie that corresponds to the provided id.
    /// </summary>
    /// <param name="id">The id for the movie to be updated.</param>
    /// <param name="movieRequest">The UpdateMovieRequest object that contains the info to be saved.</param>
    /// <response code="200">The movie was updated successfully.</response>
    /// <response code="404">A movie with the provided id was not found.</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut(ApiEndpoints.Movies.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateMovieRequest movieRequest)
    {
        var movie = movieRequest.MapToMovie(id);
        var updated = await _movieRepository.UpdateAsync(movie);
        if (!updated)
            return NotFound();

        var movieResponse = movie.MapToResponse();
        return Ok(movieResponse);
    }
    
    /// <summary>
    /// Delete the movie that has the provided id.
    /// </summary>
    /// <param name="id">The id for the movie to be deleted.</param>
    /// <response code="204">There is no movie with the requested id in the database.</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete(ApiEndpoints.Movies.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _movieRepository.DeleteByIdAsync(id);
        return NoContent();
    }
}