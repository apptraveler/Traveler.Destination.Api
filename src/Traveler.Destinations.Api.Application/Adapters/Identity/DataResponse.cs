namespace Traveler.Destinations.Api.Application.Adapters.Identity;

public class DataResponse<T>
{
    public bool Success { get; }
    public T Data { get; }

    public DataResponse(bool success, T data)
    {
        Success = success;
        Data = data;
    }
}
