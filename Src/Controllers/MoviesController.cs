using Microsoft.AspNetCore.Mvc;
using MovieAppApi.Src.Models.SearchMovies;
using MovieAppApi.Src.Views.DTO.SearchMovies;

namespace MovieAppApi.Src.Controllers;

public class MoviesController : BaseController<MoviesController>
{
  public MoviesController(ILogger<MoviesController> logger) : base(logger)
  {
  }

  [HttpGet]
  public IActionResult GetMovies([FromQuery] SearchMoviesRequestQueryDto queryDto)
  {
    var queryModel = new SearchMoviesRequestQueryModel(
      searchTerm: queryDto.search_term,
      language: queryDto.language
    );

    return Ok(queryModel);
  }
}