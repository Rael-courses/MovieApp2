namespace MovieAppApi.Src.Models.CreatePlaylist;

public class CreatePlaylistRequestModel
{
  public string Name { get; set; }
  public string? Description { get; set; }
  public ICollection<int> MovieIds { get; set; }

  public CreatePlaylistRequestModel(string name, string? description, ICollection<int> movieIds)
  {
    Name = name;
    Description = description;
    MovieIds = movieIds;
  }
}