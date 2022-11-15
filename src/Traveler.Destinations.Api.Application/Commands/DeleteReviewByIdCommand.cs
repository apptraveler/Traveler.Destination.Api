using System;
using MediatR;
using Traveler.Destinations.Api.Application.Validations;

namespace Traveler.Destinations.Api.Application.Commands;

public class DeleteReviewByIdCommand : Command<Unit>
{
    public Guid UserId { get; }
    public Guid ReviewId { get; }

    public DeleteReviewByIdCommand(Guid userId, Guid reviewId)
    {
        UserId = userId;
        ReviewId = reviewId;
    }

    public override bool IsValid()
    {
        ValidationResult = new DeleteReviewByIdCommandValidation().Validate(this);

        return ValidationResult.IsValid;
    }
}
