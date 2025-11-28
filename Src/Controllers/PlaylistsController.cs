using Microsoft.AspNetCore.Mvc;
using MovieAppApi.Src.Core.Exceptions;
using MovieAppApi.Src.Core.Services.Movie;
using MovieAppApi.Src.Models.CreatePlaylist;
using MovieAppApi.Src.Models.Movie;
using MovieAppApi.Src.Models.SearchMovies;
using MovieAppApi.Src.Views.DTO.CreatePlaylist;
using MovieAppApi.Src.Views.DTO.GetMovie;
using MovieAppApi.Src.Views.DTO.Movie;
using MovieAppApi.Src.Views.DTO.Playlist;
using MovieAppApi.Src.Views.DTO.SearchMovies;

namespace MovieAppApi.Src.Controllers;

public class PlaylistsController : BaseController<MoviesController>
{
  private readonly IPlaylistService _playlistService;
  public PlaylistsController(ILogger<MoviesController> logger, IPlaylistService playlistService) : base(logger)
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

    var model = await _playlistService.CreatePlaylistAsync(requestDto);

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