using MovieAppApi.Src.Core.Services.FetchMovies;
using MovieAppApi.Src.Models.Movie;
using MovieAppApi.Src.Models.SearchMovies;

namespace MovieAppApi.Src.Core.Services.Movie;

public class MovieService : IMovieService
{
  private readonly IFetchMoviesService _fetchMoviesService;
  public MovieService(IFetchMoviesService fetchMoviesService)
  {
    _fetchMoviesService = fetchMoviesService;
  }

  public async Task<SearchMoviesResultModel> SearchMoviesAsync(SearchMoviesRequestQueryModel query)
  {
    return await _fetchMoviesService.SearchMoviesAsync(query);
  }

  public async Task<MovieModel> GetMovieAsync(int movieId, string language)
  {
    var movieModel = await _fetchMoviesService.GetMovieAsync(movieId, language);

    return movieModel;
  }
}