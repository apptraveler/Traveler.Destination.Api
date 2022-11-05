using System;

namespace Traveler.Destinations.Api.Application.Adapters.Identity;

public class UserCredentials
{
    public Guid UserId { get; }
    public EnumerationDto AverageSpend { get; }
    public EnumerationDto[] LocationTags { get; }

    public UserCredentials(Guid userId, EnumerationDto averageSpend, EnumerationDto[] locationTags)
    {
        UserId = userId;
        AverageSpend = averageSpend;
        LocationTags = locationTags;
    }
}
