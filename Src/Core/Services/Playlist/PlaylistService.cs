using MovieAppApi.Src.Core.Repositories.Entities;
using MovieAppApi.Src.Models.CreatePlaylist;
using MovieAppApi.Src.Models.Playlist;

namespace MovieAppApi.Src.Core.Services.Playlist;

public class PlaylistService : IPlaylistService
{
  private readonly IPlaylistRepository _playlistRepository;

  public PlaylistService(
    IPlaylistRepository playlistRepository
  )
  {
    _playlistRepository = playlistRepository;
  }

  public async Task<PlaylistModel> CreatePlaylistAsync(CreatePlaylistRequestModel requestModel)
  {
    var createdPlaylistModel = await _playlistRepository.CreatePlaylistAsync(requestModel);

    return createdPlaylistModel;
  }

  public async Task<ICollection<PlaylistModel>> GetPlaylistsAsync()
  {
    return await _playlistRepository.GetPlaylistsAsync();
  }

  public async Task<PlaylistModel?> GetPlaylistAsync(int playlistId)
  {
    return await _playlistRepository.GetPlaylistAsync(playlistId);
  }

  public async Task DeletePlaylistAsync(int playlistId)
  {
    await _playlistRepository.DeletePlaylistAsync(playlistId);
  }

  public async Task<PlaylistModel?> UpdatePlaylistAsync(PlaylistModel requestModel)
  {
    return await _playlistRepository.UpdatePlaylistAsync(requestModel);
  }
}
