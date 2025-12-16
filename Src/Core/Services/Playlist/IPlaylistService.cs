using MovieAppApi.Src.Models.CreatePlaylist;
using MovieAppApi.Src.Models.Playlist;

namespace MovieAppApi.Src.Core.Services.Playlist;

public interface IPlaylistService
{
  Task<PlaylistModel> CreatePlaylistAsync(CreatePlaylistRequestModel requestModel);
  Task DeletePlaylistAsync(int playlistId);
  Task<PlaylistModel?> GetPlaylistAsync(int playlistId);
  Task<ICollection<PlaylistModel>> GetPlaylistsAsync();
}