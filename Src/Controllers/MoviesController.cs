using Microsoft.AspNetCore.Mvc;
using MovieAppApi.Src.Models.SearchMovies;
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
  public IActionResult GetMovies([FromQuery] SearchMoviesRequestQueryDto queryDto)
  {
    var queryModel = new SearchMoviesRequestQueryModel(
      searchTerm: queryDto.search_term,
      language: queryDto.language
    );

    var model = await _movieService.GetMovies(queryModel);

    var dto = new SearchMoviesResponseDto
    {
      page = 0,
      results = [],
      total_pages = 0,
      total_results = 0
    };

    return Ok(dto);
  }
}