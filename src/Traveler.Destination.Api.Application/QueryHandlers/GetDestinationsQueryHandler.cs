using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Traveler.Destination.Api.Application.Queries;
using Traveler.Destination.Api.Application.Response;
using Traveler.Destination.Api.Domain.Aggregates.DestinationAggregate;
using Traveler.Destination.Api.Domain.Exceptions;
using Traveler.Destination.Api.Domain.SeedWork;
using Traveler.Destination.Api.Infra.CrossCutting.Environments.Configurations;

namespace Traveler.Destination.Api.Application.QueryHandlers;

public class GetDestinationsQueryHandler : QueryHandler<GetDestinationsQuery, ICollection<DestinationResponse>>
{
    private readonly IDestinationRepository _destinationRepository;
    private readonly IMediator _bus;
    private readonly ILogger<GetDestinationsQueryHandler> _logger;

    public GetDestinationsQueryHandler(ApplicationConfiguration applicationConfiguration, IDestinationRepository destinationRepository, IMediator bus, ILogger<GetDestinationsQueryHandler> logger) : base(applicationConfiguration)
    {
        _destinationRepository = destinationRepository;
        _bus = bus;
        _logger = logger;
    }

    public override async Task<ICollection<DestinationResponse>> Handle(GetDestinationsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var destinations = await _destinationRepository.GetAll();

            return destinations.Select(destination => new DestinationResponse(
                    destination.Name,
                    destination.Description,
                    destination.Temperature,
                    destination.AverageSpend,
                    destination.Images,
                    destination.Route,
                    destination.Tags.Select(x => Enumeration.FromId<DestinationTags>(x.TagId)).ToList()
                )
            ).ToList();
        }
        catch (Exception e)
        {
            _logger.LogCritical("Ocorreu um erro ao buscar todos os destinos #### Exception: {0} ####", e.ToString());
            await _bus.Publish(new ExceptionNotification("12-UnknownError", "Ocorreu um erro desconhecido"), cancellationToken);
            return default;
        }
    }
}
