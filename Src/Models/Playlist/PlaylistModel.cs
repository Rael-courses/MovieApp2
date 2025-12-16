namespace MovieAppApi.Src.Models.Playlist;

public class PlaylistModel
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string? Description { get; set; }
  public ICollection<int> MovieIds { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }

  public PlaylistModel(
    int id,
    string name,
    string? description,
    ICollection<int> movieIds,
    DateTime createdAt,
    DateTime updatedAt
  )
  {
    Id = id;
    Name = name;
    Description = description;
    MovieIds = movieIds;
    CreatedAt = createdAt;
    UpdatedAt = updatedAt;
  }
}