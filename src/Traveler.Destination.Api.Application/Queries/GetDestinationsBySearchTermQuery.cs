using System.Collections.Generic;
using Traveler.Destination.Api.Application.Response;

namespace Traveler.Destination.Api.Application.Queries;

public class GetDestinationsBySearchTermQuery : Query<ICollection<DestinationResponse>>
{
    public string SearchTerm { get; }

    public GetDestinationsBySearchTermQuery(string searchTerm)
    {
        SearchTerm = searchTerm;
    }

    public override bool IsValid()
    {
        return true;
    }
}
