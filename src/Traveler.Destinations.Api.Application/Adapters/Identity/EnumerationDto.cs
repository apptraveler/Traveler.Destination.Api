namespace Traveler.Destinations.Api.Application.Adapters.Identity;

public class EnumerationDto
{
    public int Id { get; }
    public string Name { get; }

    public EnumerationDto(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
