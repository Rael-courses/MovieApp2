using MovieAppApi.Src.Models.Movie;

namespace MovieAppApi.Src.Models.SearchMovies
{
  public class SearchMoviesResultModel
  {

    public int Page { get; }
    public List<MovieModel> Results { get; }
    public int TotalPages { get; }
    public int TotalResults { get; }

    public SearchMoviesResultModel(int page, List<MovieModel> results, int totalPages, int totalResults)
    {
      Page = page;
      Results = results;
      TotalPages = totalPages;
      TotalResults = totalResults;
    }
  }
}
