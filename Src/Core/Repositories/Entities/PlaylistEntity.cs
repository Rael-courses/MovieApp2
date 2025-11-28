namespace MovieAppApi.Src.Core.Repositories.Entities;

public class PlaylistEntity
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string? Description { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public virtual ICollection<PlaylistJoinMovieEntity> PlaylistJoinMovies { get; set; }
}