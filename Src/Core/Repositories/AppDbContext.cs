using Microsoft.EntityFrameworkCore;
using MovieAppApi.Src.Core.Repositories.Entities;

namespace MovieAppApi.Src.Core.Repositories;

public class AppDbContext : DbContext
{
  public DbSet<MovieEntity> Movies { get; set; }
  public DbSet<PlaylistEntity> Playlists { get; set; }
  public DbSet<PlaylistJoinMovieEntity> PlaylistJoinMovies { get; set; }

  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {
  }


  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<MovieEntity>()
      .HasKey(x => x.Id);
    modelBuilder.Entity<MovieEntity>()
      .Property(x => x.CreatedAt)
      .IsRequired();
    modelBuilder.Entity<MovieEntity>()
      .Property(x => x.UpdatedAt)
      .IsRequired();

    modelBuilder.Entity<PlaylistEntity>()
      .HasKey(x => x.Id);
    modelBuilder.Entity<PlaylistEntity>()
      .Property(x => x.Id)
      .ValueGeneratedOnAdd();
    modelBuilder.Entity<PlaylistEntity>()
      .Property(x => x.CreatedAt)
      .IsRequired();
    modelBuilder.Entity<PlaylistEntity>()
      .Property(x => x.UpdatedAt)
      .IsRequired();
    modelBuilder.Entity<PlaylistEntity>()
      .Property(x => x.Name)
      .IsRequired();

    modelBuilder.Entity<PlaylistJoinMovieEntity>()
      .HasKey(x => new { x.PlaylistId, x.MovieId });
    modelBuilder.Entity<PlaylistJoinMovieEntity>()
      .Property(x => x.CreatedAt)
      .IsRequired();
    modelBuilder.Entity<PlaylistJoinMovieEntity>()
      .Property(x => x.UpdatedAt)
      .IsRequired();
    modelBuilder.Entity<PlaylistJoinMovieEntity>()
      .HasOne(x => x.Playlist)
      .WithMany(x => x.PlaylistJoinMovies)
      .IsRequired(true)
      .OnDelete(DeleteBehavior.Cascade);
    modelBuilder.Entity<PlaylistJoinMovieEntity>()
      .HasOne(x => x.Movie)
      .WithMany(x => x.PlaylistJoinMovies)
      .IsRequired(true)
      .OnDelete(DeleteBehavior.Cascade);
  }
}