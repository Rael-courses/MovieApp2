using System.ComponentModel.DataAnnotations;
using MovieAppApi.Src.Views.DTO.Movie;

namespace MovieAppApi.Src.Views.DTO.SearchMovies;

public class SearchMoviesResponseDto
{
  [Required] public required int page { get; init; }

  [Required] public required List<MovieDto> results { get; init; }

  [Required] public required int total_pages { get; init; }

  [Required] public required int total_results { get; init; }
}