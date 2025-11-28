using Microsoft.AspNetCore.Mvc;
using MovieAppApi.Src.Core.Exceptions;
using MovieAppApi.Src.Core.Services.Movie;
using MovieAppApi.Src.Models.Movie;
using MovieAppApi.Src.Models.SearchMovies;
using MovieAppApi.Src.Views.DTO.GetMovie;
using MovieAppApi.Src.Views.DTO.Movie;
using MovieAppApi.Src.Views.DTO.SearchMovies;

namespace MovieAppApi.Src.Controllers;

public class MoviesController : BaseController<MoviesController>
{
  private readonly IMovieService _movieService;
  public MoviesController(ILogger<MoviesController> logger, IMovieService movieService) : base(logger)
  {
    _movieService = movieService;
  }

  [HttpGet]
  public async Task<IActionResult> GetMovies([FromQuery] SearchMoviesRequestQueryDto queryDto)
  {
    var queryModel = new SearchMoviesRequestQueryModel(
      searchTerm: queryDto.search_term,
      language: queryDto.language
    );

    var model = await _movieService.SearchMoviesAsync(queryModel);

    var dto = new SearchMoviesResponseDto
    {
      page = model.Page,
      results = model.Results.Select(result => new MovieDto
      {
        id = result.Id,
        original_language = result.OriginalLanguage,
        original_title = result.OriginalTitle,
        overview = result.Overview,
        popularity = result.Popularity,
        release_date = result.ReleaseDate,
        title = result.Title,
        vote_average = result.VoteAverage,
        vote_count = result.VoteCount,
        poster_path = result.PosterPath
      }).ToList(),
      total_pages = model.TotalPages,
      total_results = model.TotalResults
    };

    return Ok(dto);
  }

  [HttpGet("{movieId}")]
  public async Task<IActionResult> GetMovieById(int movieId, [FromQuery] GetMovieRequestQueryDto queryDto)
  {
    try
    {

      MovieModel model = await _movieService.GetMovieAsync(movieId, queryDto.language);

      var dto = new MovieDto
      {
        id = model.Id,
        original_language = model.OriginalLanguage,
        original_title = model.OriginalTitle,
        overview = model.Overview,
        popularity = model.Popularity,
        release_date = model.ReleaseDate,
        title = model.Title,
        vote_average = model.VoteAverage,
        vote_count = model.VoteCount,
        poster_path = model.PosterPath
      };

      return Ok(dto);
    }
    catch (MovieNotFoundException)
    {
      return NotFound($"Movie id {movieId} not found");
    }
  }
}