using System;
using System.Collections.Generic;
using Traveler.Destination.Api.Application.Response;

namespace Traveler.Destination.Api.Application.Queries;

public class GetRecommendedDestinationsByUserIdQuery : Query<ICollection<DestinationResponse>>
{
    public Guid UserId { get; }
    public IEnumerable<object> DestinationPreference { get; }
    public IEnumerable<object> AverageSpendPreference { get; }

    public GetRecommendedDestinationsByUserIdQuery(Guid userId)
    {
        UserId = userId;
    }

    public override bool IsValid()
    {
        return true;
    }
}
