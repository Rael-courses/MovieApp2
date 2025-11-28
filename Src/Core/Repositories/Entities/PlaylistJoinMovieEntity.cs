namespace MovieAppApi.Src.Core.Repositories.Entities;

public class PlaylistJoinMovieEntity
{
  public int PlaylistId { get; set; }
  public virtual PlaylistEntity Playlist { get; set; }
  public int MovieId { get; set; }
  public virtual MovieEntity Movie { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}