using Microsoft.AspNetCore.Mvc;
using MovieAppApi.Src.Core.Services.Playlist;
using MovieAppApi.Src.Models.CreatePlaylist;
using MovieAppApi.Src.Models.Playlist;
using MovieAppApi.Src.Views.DTO.CreatePlaylist;
using MovieAppApi.Src.Views.DTO.Playlist;
using MovieAppApi.Src.Views.DTO.UpdatePlaylist;

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
      movie_ids = model.MovieIds,
      created_at = model.CreatedAt,
      updated_at = model.UpdatedAt
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
      movie_ids = model.MovieIds,
      created_at = model.CreatedAt,
      updated_at = model.UpdatedAt
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
      movie_ids = model.MovieIds,
      created_at = model.CreatedAt,
      updated_at = model.UpdatedAt
    };

    return Ok(dto);
  }

  [HttpDelete("{playlistId}")]
  public async Task<IActionResult> DeletePlaylist(int playlistId)
  {
    var existingPlaylistModel = await _playlistService.GetPlaylistAsync(playlistId);
    if (existingPlaylistModel == null)
    {
      return NotFound();
    }

    await _playlistService.DeletePlaylistAsync(playlistId);

    return NoContent();
  }

  [HttpPut("{playlistId}")]
  public async Task<IActionResult> UpdatePlaylist([FromRoute] int playlistId, [FromBody] UpdatePlaylistRequestBodyDto requestDto)
  {
    if (requestDto.id != playlistId)
    {
      return BadRequest("Playlist ID in the request body does not match the playlist ID in the route");
    }

    var requestModel = new PlaylistModel(
      id: requestDto.id,
      name: requestDto.name,
      description: requestDto.description,
      movieIds: requestDto.movie_ids,
      createdAt: requestDto.created_at,
      updatedAt: requestDto.updated_at
    );

    var updatedPlaylistModel = await _playlistService.UpdatePlaylistAsync(requestModel);

    if (updatedPlaylistModel == null)
    {
      return NotFound();
    }

    var dto = new PlaylistDto
    {
      id = updatedPlaylistModel.Id,
      name = updatedPlaylistModel.Name,
      description = updatedPlaylistModel.Description,
      movie_ids = updatedPlaylistModel.MovieIds,
      created_at = updatedPlaylistModel.CreatedAt,
      updated_at = updatedPlaylistModel.UpdatedAt
    };

    return Ok(dto);
  }


}
