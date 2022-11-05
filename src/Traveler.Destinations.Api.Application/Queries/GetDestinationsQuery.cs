using System.Collections.Generic;
using Traveler.Destinations.Api.Application.Response;

namespace Traveler.Destinations.Api.Application.Queries;

public class GetDestinationsQuery : Query<ICollection<DestinationResponse>>
{
    public override bool IsValid()
    {
        return true;
    }
}
