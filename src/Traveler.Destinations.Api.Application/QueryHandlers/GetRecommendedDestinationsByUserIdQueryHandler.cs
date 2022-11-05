using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Traveler.Destinations.Api.Application.Queries;
using Traveler.Destinations.Api.Application.Response;
using Traveler.Destinations.Api.Domain.Aggregates.DestinationAggregate;
using Traveler.Destinations.Api.Domain.Exceptions;
using Traveler.Destinations.Api.Infra.CrossCutting.Environments.Configurations;

namespace Traveler.Destinations.Api.Application.QueryHandlers;

public class GetRecommendedDestinationsByUserIdQueryHandler : QueryHandler<GetRecommendedDestinationsByUserIdQuery, ICollection<DestinationResponse>>
{
    private readonly IDestinationRepository _destinationRepository;
    private readonly IMediator _bus;
    private readonly IMapper _mapper;
    private readonly ILogger<GetRecommendedDestinationsByUserIdQueryHandler> _logger;

    public GetRecommendedDestinationsByUserIdQueryHandler(
        ApplicationConfiguration applicationConfiguration,
        IDestinationRepository destinationRepository,
        IMediator bus,
        IMapper mapper,
        ILogger<GetRecommendedDestinationsByUserIdQueryHandler> logger
    ) : base(applicationConfiguration)
    {
        _destinationRepository = destinationRepository;
        _bus = bus;
        _mapper = mapper;
        _logger = logger;
    }

    public override async Task<ICollection<DestinationResponse>> Handle(GetRecommendedDestinationsByUserIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var recommendedDestinations = await _destinationRepository.GetByPreferences(request.AverageSpendPreferenceId, request.LocationTagsIdsPreference);

            if (recommendedDestinations is null || !recommendedDestinations.Any())
            {
                await _bus.Publish(new ExceptionNotification("16-RecommendationsNotFound", "Não foi encontrado nenhuma recomendação"), cancellationToken);
                return default;
            }

            return _mapper.Map<ICollection<DestinationResponse>>(recommendedDestinations);
        }
        catch (Exception e)
        {
            _logger.LogCritical("Ocorreu um erro ao buscar os destinos favoritos do usuário #### Exception: {0} ####", e.ToString());
            await _bus.Publish(new ExceptionNotification("12-UnknownError", "Ocorreu um erro desconhecido"), cancellationToken);
            return default;
        }
    }
}
