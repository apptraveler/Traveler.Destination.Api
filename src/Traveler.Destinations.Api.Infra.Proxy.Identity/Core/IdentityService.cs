using Traveler.Destinations.Api.Application.Adapters.Identity;
using Traveler.Destinations.Api.Infra.CrossCutting.Environments.Configurations;
using Traveler.Destinations.Api.Infra.Proxy.Common.Core;

namespace Traveler.Destinations.Api.Infra.Proxy.Identity.Core;

public class IdentityService : BaseHttpRequest, IAuthorization
{
    public IdentityService(HttpClient httpClient, IdentityConfiguration identityConfiguration) : base(httpClient, identityConfiguration.BaseAddress)
    {
    }

    public async Task<UserCredentials> AuthorizeAsync(string token)
    {
        AddBearer(token);
        var response = await GetAsync<DataResponse<UserCredentials>>("/Traveler/v1/information");
        return response.Data;
    }
}
