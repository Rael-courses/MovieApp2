using Microsoft.AspNetCore.Mvc;
using MovieAppApi.Src.Core.Services.Playlist;
using MovieAppApi.Src.Models.CreatePlaylist;
using MovieAppApi.Src.Views.DTO.CreatePlaylist;
using MovieAppApi.Src.Views.DTO.Playlist;

namespace MovieAppApi.Src.Controllers;

public class PlaylistsController : BaseController<PlaylistsController>
{
  private readonly IPlaylistService _playlistService;
  public PlaylistsController(
    ILogger<PlaylistsController> logger,
    IPlaylistService playlistService
  ) : base(logger)
  {
    _playlistService = playlistService;
  }

  [HttpPost]
  public async Task<IActionResult> CreatePlaylist([FromBody] CreatePlaylistRequestBodyDto requestDto)
  {
    var requestModel = new CreatePlaylistRequestModel(
      name: requestDto.name,
      description: requestDto.description,
      movieIds: requestDto.movie_ids
    );

    var model = await _playlistService.CreatePlaylistAsync(requestModel);

    var dto = new PlaylistDto
    {
      id = model.Id,
      name = model.Name,
      description = model.Description,
      movie_ids = model.MovieIds
    };

    return CreatedAtAction("", new { id = model.Id }, dto);
  }
}