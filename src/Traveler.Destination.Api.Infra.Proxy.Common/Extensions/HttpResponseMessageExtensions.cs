namespace Traveler.Destination.Api.Infra.Proxy.Common.Extensions;

public static class HttpResponseMessageExtensions
{
    public static void EnsureRequestSuccess(this HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        var responseContent = response.Content.ReadAsStringAsync().Result;

        throw new HttpRequestException($"StatusCode: {(int) response.StatusCode}\n Content: {responseContent}");
    }
}
