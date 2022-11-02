using System;
using MediatR;
using Traveler.Destination.Api.Application.Validations;

namespace Traveler.Destination.Api.Application.Commands;

public class BookmarkDestinationByIdCommand : Command<Unit>
{
    public Guid UserId { get; }
    public Guid DestinationId { get; }

    public BookmarkDestinationByIdCommand(Guid userId, Guid destinationId)
    {
        UserId = userId;
        DestinationId = destinationId;
    }

    public override bool IsValid()
    {
        ValidationResult = new BookmarkDestinationByIdCommandValidation().Validate(this);

        return ValidationResult.IsValid;
    }
}
