using System;
using System.Collections.Generic;
using Traveler.Destinations.Api.Application.Response;

namespace Traveler.Destinations.Api.Application.Queries;

public class GetBookmarkedDestinationsQuery : Query<ICollection<DestinationResponse>>
{
    public Guid UserId { get; }

    public GetBookmarkedDestinationsQuery(Guid userId)
    {
        UserId = userId;
    }

    public override bool IsValid()
    {
        return true;
    }
}
