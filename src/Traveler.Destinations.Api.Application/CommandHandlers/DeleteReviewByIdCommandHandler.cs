using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Traveler.Destinations.Api.Application.Commands;
using Traveler.Destinations.Api.Domain.Aggregates.ReviewAggregate;
using Traveler.Destinations.Api.Domain.Exceptions;
using Traveler.Destinations.Api.Domain.SeedWork;

namespace Traveler.Destinations.Api.Application.CommandHandlers;

public class DeleteReviewByIdCommandHandler : CommandHandler<DeleteReviewByIdCommand, Unit>
{
    private readonly IReviewRepository _reviewRepository;
    private readonly ILogger<DeleteReviewByIdCommandHandler> _logger;

    public DeleteReviewByIdCommandHandler(
        IUnitOfWork uow,
        IMediator bus,
        INotificationHandler<ExceptionNotification> notifications,
        IReviewRepository reviewRepository,
        ILogger<DeleteReviewByIdCommandHandler> logger
    ) : base(uow, bus, notifications)
    {
        _reviewRepository = reviewRepository;
        _logger = logger;
    }

    public override async Task<Unit> Handle(DeleteReviewByIdCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var review = await _reviewRepository.GetById(request.ReviewId);

            if (review is null)
            {
                await Bus.Publish(new ExceptionNotification("881-ReviewNotFound", "Não foi encontrado a review"), cancellationToken);
                return Unit.Value;
            }

            if (!review.UserId.Equals(request.UserId))
            {
                await Bus.Publish(new ExceptionNotification("882-ReviewNotFromUser", "Não foi possível deletar review de outro usuário"), cancellationToken);
                return Unit.Value;
            }

            _reviewRepository.Delete(review);

            if (await CommitAsync() is false)
            {
                await Bus.Publish(new ExceptionNotification("883-ErrorOnDeleteReview", "Não foi possível processar a requisição"), cancellationToken);
                return Unit.Value;
            }

            return Unit.Value;
        }
        catch (Exception e)
        {
            _logger.LogCritical("Ocorreu ao deletar a review #### Exception: {0} ####", e.ToString());
            await Bus.Publish(new ExceptionNotification("880-UnknownError", "Não foi possível deletar a review"), cancellationToken);
            return Unit.Value;
        }
    }
}
