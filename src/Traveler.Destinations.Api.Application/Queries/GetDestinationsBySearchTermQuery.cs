using System.Collections.Generic;
using Traveler.Destinations.Api.Application.Response;
using Traveler.Destinations.Api.Application.Validations;

namespace Traveler.Destinations.Api.Application.Queries;

public class GetDestinationsBySearchTermQuery : Query<ICollection<DestinationResponse>>
{
    public string SearchTerm { get; }

    public GetDestinationsBySearchTermQuery(string searchTerm)
    {
        SearchTerm = searchTerm;
    }

    public override bool IsValid()
    {
        ValidationResult = new GetDestinationsBySearchTermQueryValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
