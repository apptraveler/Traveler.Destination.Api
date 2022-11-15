using System;
using System.Collections.Generic;
using Traveler.Destinations.Api.Application.Response;
using Traveler.Destinations.Api.Application.Validations;

namespace Traveler.Destinations.Api.Application.Queries;

public class GetReviewsByDestinationIdQuery : Query<ICollection<ReviewResponse>>
{
    public Guid DestinationId { get; }

    public GetReviewsByDestinationIdQuery(Guid destinationId)
    {
        DestinationId = destinationId;
    }

    public override bool IsValid()
    {
        ValidationResult = new GetReviewsByDestinationIdQueryValidation().Validate(this);

        return ValidationResult.IsValid;
    }
}
