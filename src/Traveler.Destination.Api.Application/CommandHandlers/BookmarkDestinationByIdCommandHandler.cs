using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Traveler.Destination.Api.Application.Commands;
using Traveler.Destination.Api.Domain.Aggregates.BookmarkedDestination;
using Traveler.Destination.Api.Domain.Aggregates.DestinationAggregate;
using Traveler.Destination.Api.Domain.Exceptions;
using Traveler.Destination.Api.Domain.SeedWork;

namespace Traveler.Destination.Api.Application.CommandHandlers;

public class BookmarkDestinationByIdCommandHandler : CommandHandler<BookmarkDestinationByIdCommand, Unit>
{
    private readonly IBookmarkDestinationRepository _bookmarkDestinationRepository;
    private readonly IDestinationRepository _destinationRepository;
    private readonly ILogger<BookmarkDestinationByIdCommand> _logger;

    public BookmarkDestinationByIdCommandHandler(
        IUnitOfWork uow,
        IMediator bus,
        INotificationHandler<ExceptionNotification> notifications,
        IBookmarkDestinationRepository bookmarkDestinationRepository,
        IDestinationRepository destinationRepository,
        ILogger<BookmarkDestinationByIdCommand> logger
    ) : base(uow, bus, notifications)
    {
        _bookmarkDestinationRepository = bookmarkDestinationRepository;
        _destinationRepository = destinationRepository;
        _logger = logger;
    }

    public override async Task<Unit> Handle(BookmarkDestinationByIdCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var destination = await _destinationRepository.GetById(request.DestinationId);

            if (destination is null)
            {
                await Bus.Publish(new ExceptionNotification("20-DestinationNotFound", "O destino não foi encontrado"), cancellationToken);
                return Unit.Value;
            }

            var bookmarkedDestination = await _bookmarkDestinationRepository.GetSpecificDestinationByUserId(request.DestinationId, request.UserId);

            if (bookmarkedDestination is not null)
            {
                await Bus.Publish(new ExceptionNotification("20-DestinationAlreadyBookmarked", "O destino já foi salvo como favorito"), cancellationToken);
                return Unit.Value;
            }

            var destinationToBookmark = new BookmarkedDestination(request.UserId, request.DestinationId);

            _bookmarkDestinationRepository.Add(destinationToBookmark);

            if (await CommitAsync() is false)
            {
                await Bus.Publish(new ExceptionNotification("20-ErrorToProcessRequest", "Não foi possível processar a requisição"), cancellationToken);
                return Unit.Value;
            }

            return Unit.Value;
        }
        catch (Exception e)
        {
            _logger.LogCritical("Ocorreu um erro ao salvar o destino #### {0} ####", e.ToString());
            await Bus.Publish(new ExceptionNotification("20-ErrorNotKnown", "Ocorreu um erro desconhecido"), cancellationToken);
            return Unit.Value;
        }
    }
}
