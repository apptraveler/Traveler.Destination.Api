namespace Traveler.Destinations.Api.Dtos.Request;

public class CreateReviewRequest
{
    public int Rate { get; }
    public string Message { get; }

    public CreateReviewRequest(int rate, string message)
    {
        Rate = rate;
        Message = message;
    }
}
