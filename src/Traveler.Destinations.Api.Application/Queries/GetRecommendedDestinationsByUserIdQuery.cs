using System;
using System.Collections.Generic;
using Traveler.Destinations.Api.Application.Response;
using Traveler.Destinations.Api.Application.Validations;

namespace Traveler.Destinations.Api.Application.Queries;

public class GetRecommendedDestinationsByUserIdQuery : Query<ICollection<DestinationResponse>>
{
    public Guid UserId { get; }
    public ICollection<int> LocationTagsIdsPreference { get; }
    public int AverageSpendPreferenceId { get; }

    public GetRecommendedDestinationsByUserIdQuery(Guid userId, ICollection<int> locationTagsIdsPreference, int averageSpendPreferenceId)
    {
        UserId = userId;
        LocationTagsIdsPreference = locationTagsIdsPreference;
        AverageSpendPreferenceId = averageSpendPreferenceId;
    }

    public override bool IsValid()
    {
        ValidationResult = new GetRecommendedDestinationsByUserIdQueryValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}
