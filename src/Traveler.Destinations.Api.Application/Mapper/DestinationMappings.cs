using System.Linq;
using Traveler.Destinations.Api.Application.Response;
using Traveler.Destinations.Api.Domain.Aggregates.DestinationAggregate;
using Traveler.Destinations.Api.Domain.SeedWork;

namespace Traveler.Destinations.Api.Application.Mapper;

public class DestinationMappings : MappingProfile
{
    public DestinationMappings()
    {
        CreateMap<Destinations.Api.Domain.Aggregates.DestinationAggregate.Destination, DestinationResponse>()
            .ConvertUsing(destination =>
                new DestinationResponse(
                    destination.Id,
                    destination.Name,
                    destination.City,
                    destination.Country,
                    destination.Description,
                    new ClimateAverageDto(destination.ClimateAverage.Min, destination.ClimateAverage.Max, Enumeration.FromId<ClimateStatus>(destination.ClimateAverage.StatusId)),
                    destination.AverageSpend,
                    destination.Images.Select(x => x.Url).ToList(),
                    destination.Route.Select(x => new RouteCoordinateDto(x.Name, x.Description, x.Latitude, x.Longitude)).ToList(),
                    destination.Tags.Select(x => Enumeration.FromId<DestinationTags>(x.TagId)).ToList()
                )
            );
    }
}
