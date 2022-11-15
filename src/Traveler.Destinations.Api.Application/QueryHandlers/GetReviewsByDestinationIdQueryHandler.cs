using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Traveler.Destinations.Api.Application.Queries;
using Traveler.Destinations.Api.Application.Response;
using Traveler.Destinations.Api.Domain.Aggregates.ReviewAggregate;
using Traveler.Destinations.Api.Domain.Exceptions;
using Traveler.Destinations.Api.Infra.CrossCutting.Environments.Configurations;

namespace Traveler.Destinations.Api.Application.QueryHandlers;

public class GetReviewsByDestinationIdQueryHandler : QueryHandler<GetReviewsByDestinationIdQuery, ICollection<ReviewResponse>>
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _bus;
    private readonly ILogger<GetReviewsByDestinationIdQueryHandler> _logger;

    public GetReviewsByDestinationIdQueryHandler(
        ApplicationConfiguration applicationConfiguration,
        IReviewRepository reviewRepository,
        IMapper mapper,
        IMediator bus,
        ILogger<GetReviewsByDestinationIdQueryHandler> logger
    ) : base(applicationConfiguration)
    {
        _reviewRepository = reviewRepository;
        _mapper = mapper;
        _bus = bus;
        _logger = logger;
    }

    public override async Task<ICollection<ReviewResponse>> Handle(GetReviewsByDestinationIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var reviews = await _reviewRepository.GetByDestinationId(request.DestinationId);

            return _mapper.Map<ICollection<ReviewResponse>>(reviews);
        }
        catch (Exception e)
        {
            _logger.LogCritical("Ocorreu um erro ao buscar as reviews do destino #### Exception: {0} ####", e.ToString());
            await _bus.Publish(new ExceptionNotification("430-UnknownError", "Ocorreu um erro ao buscar as reviews"), cancellationToken);
            return default;
        }
    }
}
