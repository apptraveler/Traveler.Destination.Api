using System;

namespace Traveler.Destinations.Api.Application.Response;

public class ReviewResponse
{
    public Guid Id { get; }
    public string ReviewerName { get; }
    public string Message { get; }
    public int Rate { get; }

    public ReviewResponse(Guid id, string reviewerName, string message, int rate)
    {
        Id = id;
        ReviewerName = reviewerName;
        Message = message;
        Rate = rate;
    }
}
