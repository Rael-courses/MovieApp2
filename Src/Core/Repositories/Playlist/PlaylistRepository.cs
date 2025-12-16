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
    var newMovieEntities = await GetNewMovieEntitiesToAddAsync(requestModel.MovieIds);
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

  public async Task<ICollection<PlaylistModel>> GetPlaylistsAsync()
  {
    var playlistsEntities = await _appDbContext.Playlists
      .Include(p => p.PlaylistJoinMovies)
      .ToListAsync();

    return playlistsEntities.Select(p => new PlaylistModel(
      p.Id,
      p.Name,
      p.Description,
      p.PlaylistJoinMovies.Select(pjm => pjm.MovieId).ToList(),
      p.CreatedAt,
      p.UpdatedAt
    )).ToList();
  }

  public async Task<PlaylistModel?> GetPlaylistAsync(int playlistId)
  {
    var playlistEntity = await _appDbContext.Playlists
      .Include(p => p.PlaylistJoinMovies)
      .FirstOrDefaultAsync(p => p.Id == playlistId);

    if (playlistEntity == null)
    {
      return null;
    }

    var model = new PlaylistModel(
      playlistEntity.Id,
      playlistEntity.Name,
      playlistEntity.Description,
      playlistEntity.PlaylistJoinMovies.Select(pjm => pjm.MovieId).ToList(),
      playlistEntity.CreatedAt,
      playlistEntity.UpdatedAt
    );

    return model;
  }

  public async Task DeletePlaylistAsync(int playlistId)
  {
    var playlistEntity = await _appDbContext.Playlists
      .FirstOrDefaultAsync(p => p.Id == playlistId);
    if (playlistEntity == null)
    {
      throw new Exception("Playlist not found");
    }

    _appDbContext.Playlists.Remove(playlistEntity);
    await _appDbContext.SaveChangesAsync();
  }

  public async Task<PlaylistModel?> UpdatePlaylistAsync(PlaylistModel requestModel)
  {
    var playlistEntity = await _appDbContext.Playlists
      .Include(p => p.PlaylistJoinMovies)
      .FirstOrDefaultAsync(p => p.Id == requestModel.Id);
    if (playlistEntity == null)
    {
      return null;
    }

    // Update playlist entity primary properties
    playlistEntity.Name = requestModel.Name;
    playlistEntity.Description = requestModel.Description;
    playlistEntity.UpdatedAt = DateTime.UtcNow;

    var playlistJoinMovieEntitiesToRemove = playlistEntity.PlaylistJoinMovies
      .Where(pjm => !requestModel.MovieIds.Contains(pjm.MovieId))
      .ToList();
    _appDbContext.PlaylistJoinMovies.RemoveRange(playlistJoinMovieEntitiesToRemove);


    // Prepare new movie entities to add if not already in database
    var newMovieEntities = await GetNewMovieEntitiesToAddAsync(requestModel.MovieIds);
    await _appDbContext.Movies.AddRangeAsync(newMovieEntities);

    // Prepare playlist join movie entities
    var playlistJoinMovieEntitiesToAdd = requestModel.MovieIds
      .Where(movieId => !playlistJoinMovieEntitiesToRemove.Any(pjm => pjm.MovieId == movieId))
      .Select(movieId => new PlaylistJoinMovieEntity
      {
        PlaylistId = playlistEntity.Id,
        MovieId = movieId,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
      }).ToList();
    await _appDbContext.PlaylistJoinMovies.AddRangeAsync(playlistJoinMovieEntitiesToAdd);

    await _appDbContext.SaveChangesAsync();

    return new PlaylistModel(
      playlistEntity.Id,
      playlistEntity.Name,
      playlistEntity.Description,
      requestModel.MovieIds,
      playlistEntity.CreatedAt,
      playlistEntity.UpdatedAt
    );
  }

  private async Task<ICollection<MovieEntity>> GetNewMovieEntitiesToAddAsync(ICollection<int> movieIds)
  {
    var existingMovieEntities = await _appDbContext.Movies
      .Where(m => movieIds.Contains(m.Id))
      .ToListAsync();

    var newMovieEntities = movieIds
      .Where(id => !existingMovieEntities.Any(m => m.Id == id))
      .Select(id => new MovieEntity
      {
        Id = id,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
      }).ToList();

    return newMovieEntities;
  }
}