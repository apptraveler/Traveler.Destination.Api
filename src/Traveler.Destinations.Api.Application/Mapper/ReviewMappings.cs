using Traveler.Destinations.Api.Application.Response;
using Traveler.Destinations.Api.Domain.Aggregates.ReviewAggregate;

namespace Traveler.Destinations.Api.Application.Mapper;

public class ReviewMappings : MappingProfile
{
    public ReviewMappings()
    {
        CreateMap<Review, ReviewResponse>()
            .ConvertUsing(review => new ReviewResponse(review.Id, review.ReviewerName, review.Message, review.Rate.Value));
    }
}
