using System.ComponentModel.DataAnnotations;

namespace MovieAppApi.Src.Views.DTO.CreatePlaylist;

public class CreatePlaylistRequestBodyDto
{
  [Required(AllowEmptyStrings = false)]
  public required string name { get; init; }
  public string? description { get; init; }
  [MinLength(1)]
  public required ICollection<int> movie_ids { get; init; }
}