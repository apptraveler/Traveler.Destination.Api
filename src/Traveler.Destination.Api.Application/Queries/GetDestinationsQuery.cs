using System.Collections.Generic;
using Traveler.Destination.Api.Application.Response;

namespace Traveler.Destination.Api.Application.Queries;

public class GetDestinationsQuery : Query<ICollection<DestinationResponse>>
{
    public override bool IsValid()
    {
        return true;
    }
}
