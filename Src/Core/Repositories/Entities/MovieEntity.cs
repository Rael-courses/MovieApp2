namespace MovieAppApi.Src.Core.Repositories.Entities;

public class MovieEntity
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public virtual ICollection<PlaylistJoinMovieEntity> PlaylistJoinMovies { get; set; }
}