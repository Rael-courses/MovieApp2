using System.ComponentModel.DataAnnotations;
using MovieAppApi.Src.Models.SearchMovies;

namespace MovieAppApi.Src.Core.Services.FetchMovies.Tmdb;

public class TmdbSearchMoviesResultDto
{
  [Required] public required int page { get; init; }
  [Required] public required List<TmdbMovieDto> results { get; init; }
  [Required] public required int total_pages { get; init; }
  [Required] public required int total_results { get; init; }

  public SearchMoviesResultModel ToModel()
  {
    Validator.ValidateObject(this, new ValidationContext(this), true);

    return new SearchMoviesResultModel(
        page: page,
        results: results.Select(movieDto => movieDto.ToModel()).ToList(),
        totalPages: total_pages,
        totalResults: total_results
    );
  }
}