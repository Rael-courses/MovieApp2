using Microsoft.EntityFrameworkCore;
using MovieAppApi.Src.Core.Repositories.Entities;
using MovieAppApi.Src.Core.Services.Playlist;
using MovieAppApi.Src.Models.CreatePlaylist;
using MovieAppApi.Src.Models.Playlist;

namespace MovieAppApi.Src.Core.Repositories.Playlist;

public class PlaylistRepository : IPlaylistRepository
{
  private readonly AppDbContext _appDbContext;

  public PlaylistRepository(AppDbContext appDbContext)
  {
    _appDbContext = appDbContext;
  }

  public async Task<PlaylistModel> CreatePlaylistAsync(CreatePlaylistRequestModel requestModel)
  {
    // Prepare playlist entity
    var playlistEntity = new PlaylistEntity
    {
      Name = requestModel.Name,
      Description = requestModel.Description,
      CreatedAt = DateTime.UtcNow,
      UpdatedAt = DateTime.UtcNow
    };
    await _appDbContext.Playlists.AddAsync(playlistEntity);

    // Prepare movie entities
    var existingMovieEntities = await _appDbContext.Movies
      .Where(m => requestModel.MovieIds.Contains(m.Id))
      .ToListAsync();

    var newMovieEntities = requestModel.MovieIds
      .Where(id => !existingMovieEntities.Any(m => m.Id == id))
      .Select(id => new MovieEntity
      {
        Id = id,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
      });
    await _appDbContext.Movies.AddRangeAsync(newMovieEntities);

    // Prepare playlist join movie entities
    var playlistJoinMovieEntities = requestModel.MovieIds.Select(movieId => new PlaylistJoinMovieEntity
    {
      // We set the playlist entity to allow the EF Core to track the relationship
      Playlist = playlistEntity,
      // We don't need to track the movie entity because the Id is not auto-generated and we created the movie entry just before
      MovieId = movieId,
      CreatedAt = DateTime.UtcNow,
      UpdatedAt = DateTime.UtcNow
    }).ToList();
    await _appDbContext.PlaylistJoinMovies.AddRangeAsync(playlistJoinMovieEntities);

    await _appDbContext.SaveChangesAsync();

    return new PlaylistModel(
      playlistEntity.Id,
      playlistEntity.Name,
      playlistEntity.Description,
      // We simply set the movie ids from the request because they were actually added to the database
      requestModel.MovieIds,
      playlistEntity.CreatedAt,
      playlistEntity.UpdatedAt
    );
  }
}
