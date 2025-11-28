using MovieAppApi.Src.Core.Services.Environment;
using MovieAppApi.Src.Models.SearchMovies;

namespace MovieAppApi.Src.Core.Services.FetchMovies.Tmdb;

public class TmdbService : IFetchMoviesService
{
  private readonly HttpClient _httpClient;
  private readonly string _apiKey;
  private readonly string _baseUrl;

  public TmdbService(HttpClient httpClient, IEnvService envService)
  {
    _httpClient = httpClient;
    _apiKey = envService.Vars.TmdbApiKey;
    _baseUrl = "https://api.themoviedb.org/3";
  }

  public async Task<SearchMoviesResultModel> SearchMoviesAsync(SearchMoviesRequestQueryModel query)
  {
    var url = $"{_baseUrl}/search/movie?api_key={_apiKey}&query={query.SearchTerm}&language={query.Language}";
    var response = await _httpClient.GetAsync(url);
    if (!response.IsSuccessStatusCode)
    {
      throw new HttpRequestException("Tmdb search movies request failed");
    }

    var dto = await response.Content.ReadFromJsonAsync<TmdbSearchMoviesResultDto>();
    if (dto == null)
    {
      throw new NullReferenceException("Tmdb search movies response is null");
    }

    return dto.ToModel();
  }
}
