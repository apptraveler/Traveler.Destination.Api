using System;

namespace Traveler.Destinations.Api.Application.Adapters.Identity;

public class UserCredentials
{
    public Guid UserId { get; }
    public string FullName { get; }
    public EnumerationDto AverageSpend { get; }
    public EnumerationDto[] LocationTags { get; }

    public UserCredentials(Guid userId, string fullName, EnumerationDto averageSpend, EnumerationDto[] locationTags)
    {
        UserId = userId;
        FullName = fullName;
        AverageSpend = averageSpend;
        LocationTags = locationTags;
    }
}
