using System.Collections.Generic;
using Traveler.Destination.Api.Application.Response;
using Traveler.Destination.Api.Application.Validations;

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
        ValidationResult = new GetDestinationsBySearchTermQueryValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
