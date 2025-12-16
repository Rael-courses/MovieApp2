using System.ComponentModel.DataAnnotations;

namespace MovieAppApi.Src.Views.DTO.UpdatePlaylist;

public class UpdatePlaylistRequestBodyDto
{
  [Required]
  public int id { get; init; }

  [Required(AllowEmptyStrings = false)]
  public required string name { get; init; }

  public string? description { get; init; }

  [MinLength(1)]
  public required ICollection<int> movie_ids { get; init; }

  [Required]
  public DateTime created_at { get; init; }

  [Required]
  public DateTime updated_at { get; init; }
}