using MovieAppApi.Src.Models.CreatePlaylist;
using MovieAppApi.Src.Models.Playlist;

namespace MovieAppApi.Src.Core.Services.Playlist;

public interface IPlaylistRepository
{
  Task<PlaylistModel> CreatePlaylistAsync(CreatePlaylistRequestModel requestModel);
}