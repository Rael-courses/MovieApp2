using Microsoft.AspNetCore.Mvc;
using MovieAppApi.Src.Core.Services.Playlist;
using MovieAppApi.Src.Models.CreatePlaylist;
using MovieAppApi.Src.Models.Playlist;
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

    return CreatedAtAction(nameof(GetPlaylist), new { playlistId = model.Id }, dto);
  }

  [HttpGet]
  public async Task<IActionResult> GetPlaylists()
  {
    var models = await _playlistService.GetPlaylistsAsync();

    var dto = models.Select(model => new PlaylistDto
    {
      id = model.Id,
      name = model.Name,
      description = model.Description,
      movie_ids = model.MovieIds
    }).ToList();

    return Ok(dto);
  }

  [HttpGet("{playlistId}")]
  public async Task<IActionResult> GetPlaylist(int playlistId)
  {
    var model = await _playlistService.GetPlaylistAsync(playlistId);

    if (model == null)
    {
      return NotFound();
    }

    var dto = new PlaylistDto
    {
      id = model.Id,
      name = model.Name,
      description = model.Description,
      movie_ids = model.MovieIds
    };

    return Ok(dto);
  }
}