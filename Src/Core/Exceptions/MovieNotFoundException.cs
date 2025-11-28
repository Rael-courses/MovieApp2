namespace MovieAppApi.Src.Core.Exceptions;

public class MovieNotFoundException : Exception
{
  public MovieNotFoundException(int id) : base($"Movie id {id} not found")
  {
  }
}