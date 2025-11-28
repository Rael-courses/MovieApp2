using MovieAppApi.Src.Core.Services.Environment;
using MovieAppApi.Src.Core.Services.FetchMovies;
using MovieAppApi.Src.Core.Services.FetchMovies.Tmdb;
using MovieAppApi.Src.Core.Services.Movie;

namespace MovieAppApi;

public class Program
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddSingleton<IEnvService>(new EnvService());

    // Register services with HttpClientFactory
    builder.Services.AddScoped<IMovieService, MovieService>();
    builder.Services.AddHttpClient<IFetchMoviesService, TmdbService>();

    builder.Services.AddControllers();

    // Add Swagger/OpenAPI support
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure Swagger middleware
    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
  }
}
