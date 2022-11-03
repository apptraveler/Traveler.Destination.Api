using System.Net.Http.Headers;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Traveler.Destination.Api.Infra.Proxy.Common.Extensions;

namespace Traveler.Destination.Api.Infra.Proxy.Common.Core;

public abstract class BaseHttpRequest
{
  protected readonly HttpClient HttpClient;

    protected BaseHttpRequest(HttpClient httpClient, string baseUrl)
    {
        HttpClient = httpClient;
        HttpClient.BaseAddress = new Uri(baseUrl);
    }

    protected void AddBasic(string username, string password)
    {
        var encodedCredentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + password));

        HttpClient.DefaultRequestHeaders.Clear();
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedCredentials);
    }

    protected void AddBearer(string token)
    {
        HttpClient.DefaultRequestHeaders.Clear();
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    protected async Task<TResponse> GetAsync<TResponse>(string path)
    {
        var response = await HttpClient.GetAsync(path);

        response.EnsureRequestSuccess();

        return await ReadAsAsync<TResponse>(response.Content);
    }

    protected async Task<TResponse> PostAsync<TRequest, TResponse>(string path, TRequest content, string mediaType = MediaTypeNames.Application.Json)
    {
        var serializedContent = JsonSerializer.Serialize(content);

        var encodedRequestData = new StringContent(serializedContent, Encoding.UTF8, mediaType);

        var response = await HttpClient.PostAsync(path, encodedRequestData);

        response.EnsureRequestSuccess();

        return await ReadAsAsync<TResponse>(response.Content);
    }

    protected async Task PostAsync(string path, object content, string mediaType = MediaTypeNames.Application.Json)
    {
        var serializedContent = JsonSerializer.Serialize(content);

        var encodedRequestData = new StringContent(serializedContent, Encoding.UTF8, mediaType);

        var response = await HttpClient.PostAsync(path, encodedRequestData);

        response.EnsureRequestSuccess();
    }

    private static async Task<T> ReadAsAsync<T>(HttpContent httpContent)
    {
        var genericProperties = typeof(T).GetProperties();
        var nestedGenericProperties = typeof(T).GetGenericArguments().SelectMany(x => x.GetProperties());
        var anyGenericHasJsonPropertyNameAttribute = genericProperties.Any(x => x.IsDefined(typeof(JsonPropertyNameAttribute))) || nestedGenericProperties.Any(x => x.IsDefined(typeof(JsonPropertyNameAttribute)));

        if (!anyGenericHasJsonPropertyNameAttribute) return await httpContent.ReadAsAsync<T>();

        var contentAsString = await httpContent.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(contentAsString)!;
    }
}
