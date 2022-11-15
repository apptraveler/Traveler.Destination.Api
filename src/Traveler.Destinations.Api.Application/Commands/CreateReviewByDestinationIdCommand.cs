using System;
using Traveler.Destinations.Api.Application.Response;
using Traveler.Destinations.Api.Application.Validations;

namespace Traveler.Destinations.Api.Application.Commands;

public class CreateReviewByDestinationIdCommand : Command<ReviewResponse>
{
    public Guid DestinationId { get; }
    public Guid UserId { get; }
    public string ReviewerName { get; }
    public string Message { get; }
    public int Rate { get; }

    public CreateReviewByDestinationIdCommand(Guid destinationId, Guid userId, string reviewerName, string message, int rate)
    {
        DestinationId = destinationId;
        UserId = userId;
        ReviewerName = reviewerName;
        Message = message;
        Rate = rate;
    }

    public override bool IsValid()
    {
        ValidationResult = new CreateReviewByDestinationIdCommandValidation().Validate(this);

        return ValidationResult.IsValid;
    }
}
