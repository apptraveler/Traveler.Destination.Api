using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Traveler.Destinations.Api.Application.Commands;
using Traveler.Destinations.Api.Application.Response;
using Traveler.Destinations.Api.Domain.Aggregates.DestinationAggregate;
using Traveler.Destinations.Api.Domain.Aggregates.ReviewAggregate;
using Traveler.Destinations.Api.Domain.Exceptions;
using Traveler.Destinations.Api.Domain.SeedWork;

namespace Traveler.Destinations.Api.Application.CommandHandlers;

public class CreateReviewByDestinationIdCommandHandler : CommandHandler<CreateReviewByDestinationIdCommand, ReviewResponse>
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IDestinationRepository _destinationRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateReviewByDestinationIdCommandHandler> _logger;

    public CreateReviewByDestinationIdCommandHandler(
        IUnitOfWork uow,
        IMediator bus,
        INotificationHandler<ExceptionNotification> notifications,
        IReviewRepository reviewRepository,
        IDestinationRepository destinationRepository,
        IMapper mapper,
        ILogger<CreateReviewByDestinationIdCommandHandler> logger
    ) : base(uow, bus, notifications)
    {
        _reviewRepository = reviewRepository;
        _destinationRepository = destinationRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public override async Task<ReviewResponse> Handle(CreateReviewByDestinationIdCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var destination = await _destinationRepository.GetById(request.DestinationId);

            if (destination is null)
            {
                await Bus.Publish(new ExceptionNotification("781-DestinationNotFound", "O destino não foi encontrado"), cancellationToken);
                return default;
            }

            var review = new Review(request.UserId, request.DestinationId,  request.ReviewerName, request.Message, request.Rate);

            _reviewRepository.Add(review);

            if (await CommitAsync() is false)
            {
                await Bus.Publish(new ExceptionNotification("782-ErrorOnSavingReview", "Não foi possível salvar sua review"), cancellationToken);
                return default;
            }

            return _mapper.Map<ReviewResponse>(review);
        }
        catch (Exception e)
        {
            _logger.LogCritical("Ocorreu um erro ao criar a review do destino #### Exception: {0} ####", e.ToString());
            await Bus.Publish(new ExceptionNotification("780-UnknownError", "Ocorreu um erro inesperado"), cancellationToken);
            return default;
        }
    }
}
