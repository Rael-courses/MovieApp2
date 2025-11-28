namespace MovieAppApi.Src.Models.SearchMovies;

public class SearchMoviesRequestQueryModel
{
  public string SearchTerm { get; }
  public string Language { get; }

  public SearchMoviesRequestQueryModel(string searchTerm, string language)
  {
    SearchTerm = searchTerm;
    Language = language;
  }
}