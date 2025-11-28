using System.ComponentModel.DataAnnotations;

namespace MovieAppApi.Src.Views.DTO.SearchMovies;

public class SearchMoviesRequestQueryDto
{
  [Required(AllowEmptyStrings = false)]
  public required string search_term { get; init; }

  [Required, AllowedValues("en", "fr", ErrorMessage = "Invalid language, must be one of: en, fr")]
  public required string language { get; init; }

}