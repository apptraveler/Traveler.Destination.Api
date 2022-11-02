using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Traveler.Destination.Api.Application.Commands;
using Traveler.Destination.Api.Domain.Aggregates.BookmarkedDestination;
using Traveler.Destination.Api.Domain.Exceptions;
using Traveler.Destination.Api.Domain.SeedWork;

namespace Traveler.Destination.Api.Application.CommandHandlers;

public class DeleteBookmarkedDestinationByIdCommandHandler : CommandHandler<DeleteBookmarkedDestinationByIdCommand, Unit>
{
    private readonly IBookmarkDestinationRepository _bookmarkDestinationRepository;
    private readonly ILogger<DeleteBookmarkedDestinationByIdCommandHandler> _logger;

    public DeleteBookmarkedDestinationByIdCommandHandler(
        IUnitOfWork uow,
        IMediator bus,
        INotificationHandler<ExceptionNotification> notifications,
        IBookmarkDestinationRepository bookmarkDestinationRepository,
        ILogger<DeleteBookmarkedDestinationByIdCommandHandler> logger
    ) : base(uow, bus, notifications)
    {
        _bookmarkDestinationRepository = bookmarkDestinationRepository;
        _logger = logger;
    }

    public override async Task<Unit> Handle(DeleteBookmarkedDestinationByIdCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var bookmarkedDestination = await _bookmarkDestinationRepository.GetSpecificDestinationByUserId(request.DestinationId, request.UserId);

            if (bookmarkedDestination is null)
            {
                await Bus.Publish(new ExceptionNotification("10-BookmarkedDestinationNotFound", "O destino não foi encontrado nos favoritos"), cancellationToken);
                return Unit.Value;
            }

            _bookmarkDestinationRepository.Remove(bookmarkedDestination);

            if (await CommitAsync() is false)
            {
                await Bus.Publish(new ExceptionNotification("10-ErrorOnProcessingRequest", "Ocorreu um erro ao processar sua requisição"), cancellationToken);
                return Unit.Value;
            }

            return Unit.Value;
        }
        catch (Exception e)
        {
            _logger.LogCritical("Ocorreu um erro ao deletar o favorito #### Exception: {0} ####", e.ToString());
            await Bus.Publish(new ExceptionNotification("10-UnknownError", "Ocorreu um erro desconhecido"), cancellationToken);
            return Unit.Value;
        }
    }
}
