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
using Traveler.Destination.Api.Domain.Aggregates.BookmarkedDestination;
using Traveler.Destination.Api.Domain.Aggregates.DestinationAggregate;
using Traveler.Destination.Api.Domain.Exceptions;
using Traveler.Destination.Api.Domain.SeedWork;
using Traveler.Destination.Api.Infra.CrossCutting.Environments.Configurations;

namespace Traveler.Destination.Api.Application.QueryHandlers;

public class GetBookmarkedDestinationsQueryHandler : QueryHandler<GetBookmarkedDestinationsQuery, ICollection<DestinationResponse>>
{
    private readonly IBookmarkDestinationRepository _bookmarkDestinationRepository;
    private readonly IDestinationRepository _destinationRepository;
    private readonly IMediator _bus;
    private readonly IMapper _mapper;
    private readonly ILogger<GetBookmarkedDestinationsQueryHandler> _logger;

    public GetBookmarkedDestinationsQueryHandler(
        ApplicationConfiguration applicationConfiguration,
        IBookmarkDestinationRepository bookmarkDestinationRepository,
        IDestinationRepository destinationRepository,
        IMediator bus,
        IMapper mapper,
        ILogger<GetBookmarkedDestinationsQueryHandler> logger
    ) : base(applicationConfiguration)
    {
        _bookmarkDestinationRepository = bookmarkDestinationRepository;
        _destinationRepository = destinationRepository;
        _bus = bus;
        _mapper = mapper;
        _logger = logger;
    }

    public override async Task<ICollection<DestinationResponse>> Handle(Queries.GetBookmarkedDestinationsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var bookmarkedDestinations = await _bookmarkDestinationRepository.GetAllByUserId(request.UserId);

            if (bookmarkedDestinations is null) return default;

            var destinations = new List<Domain.Aggregates.DestinationAggregate.Destination>();

            foreach (var bookmarkedDestination in bookmarkedDestinations)
            {
                try
                {
                    var destination = await _destinationRepository.GetById(bookmarkedDestination.DestinationId);
                    destinations.Add(destination);
                }
                catch (Exception e)
                {
                    _logger.LogCritical("Destino não encontrado, tentanto buscar o restante");
                }
            }

            return _mapper.Map<ICollection<DestinationResponse>>(destinations);
        }
        catch (Exception e)
        {
            _logger.LogCritical("Ocorreu um erro ao buscar os destinos favoritos do usuário #### Exception: {0} ####", e.ToString());
            await _bus.Publish(new ExceptionNotification("12-UnknownError", "Ocorreu um erro desconhecido"), cancellationToken);
            return default;
        }
    }
}
