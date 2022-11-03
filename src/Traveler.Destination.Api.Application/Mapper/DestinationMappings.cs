using System.Linq;
using Traveler.Destination.Api.Application.Response;
using Traveler.Destination.Api.Domain.Aggregates.DestinationAggregate;
using Traveler.Destination.Api.Domain.SeedWork;

namespace Traveler.Destination.Api.Application.Mapper;

public class DestinationMappings : MappingProfile
{
    public DestinationMappings()
    {
        CreateMap<Domain.Aggregates.DestinationAggregate.Destination, DestinationResponse>()
            .ConvertUsing(destination =>
                new DestinationResponse(
                    destination.Name,
                    destination.Description,
                    new ClimateAverageDto(destination.ClimateAverage.Min, destination.ClimateAverage.Max, Enumeration.FromId<ClimateStatus>(destination.ClimateAverage.StatusId)),
                    destination.AverageSpend,
                    destination.Images.Select(x => x.Url).ToList(),
                    destination.Route.Select(x => new RouteCoordinateDto(x.Latitude, x.Longitude)).ToList(),
                    destination.Tags.Select(x => Enumeration.FromId<DestinationTags>(x.TagId)).ToList()
                )
            );
    }
}
