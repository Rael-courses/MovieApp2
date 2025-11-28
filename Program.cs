using MovieAppApi.Src.Core.Services.Environment;

namespace MovieAppApi;

public class Program
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddSingleton<IEnvService>(new EnvService());

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
