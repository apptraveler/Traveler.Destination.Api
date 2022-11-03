using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Traveler.Destination.Api.Application.Queries;
using Traveler.Destination.Api.Application.Response;
using Traveler.Destination.Api.Domain.Aggregates.DestinationAggregate;
using Traveler.Destination.Api.Domain.Exceptions;
using Traveler.Destination.Api.Infra.CrossCutting.Environments.Configurations;

namespace Traveler.Destination.Api.Application.QueryHandlers;

public class GetDestinationsBySearchTermQueryHandler : QueryHandler<GetDestinationsBySearchTermQuery, ICollection<DestinationResponse>>
{
    private readonly IDestinationRepository _destinationRepository;
    private readonly IMediator _bus;
    private readonly IMapper _mapper;
    private readonly ILogger<GetDestinationsBySearchTermQueryHandler> _logger;

    public GetDestinationsBySearchTermQueryHandler(
        ApplicationConfiguration applicationConfiguration,
        IDestinationRepository destinationRepository,
        IMediator bus,
        IMapper mapper,
        ILogger<GetDestinationsBySearchTermQueryHandler> logger
    ) : base(applicationConfiguration)
    {
        _destinationRepository = destinationRepository;
        _bus = bus;
        _mapper = mapper;
        _logger = logger;
    }

    public override async Task<ICollection<DestinationResponse>> Handle(GetDestinationsBySearchTermQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var destinations = await _destinationRepository.GetBySearchTerm(request.SearchTerm);

            return _mapper.Map<ICollection<DestinationResponse>>(destinations);
        }
        catch (Exception e)
        {
            _logger.LogCritical("Ocorreu um erro ao buscar os destinos com base no termo #### Exception: {0} ####", e.ToString());
            await _bus.Publish(new ExceptionNotification("12-UnknownError", "Ocorreu um erro desconhecido"), cancellationToken);
            return default;
        }
    }
}
