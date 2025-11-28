namespace MovieAppApi.Src.Core.Services.Environment;

public interface IVariables
{
  public string TmdbApiKey { get; }
  public string DatabaseUrl { get; }
}
