using Traveler.Destination.Api.Application.Adapters.Identity;
using Traveler.Destination.Api.Infra.CrossCutting.Environments.Configurations;
using Traveler.Destination.Api.Infra.Proxy.Common.Core;

namespace Traveler.Destination.Api.Infra.Proxy.Identity.Core;

public class IdentityService : BaseHttpRequest, IAuthorization
{
    public IdentityService(HttpClient httpClient, IdentityConfiguration identityConfiguration) : base(httpClient, identityConfiguration.BaseAddress)
    {
    }

    public async Task<UserCredentials> AuthorizeAsync(string token)
    {
        AddBearer(token);
        // TODO: verificar path
        var userCredentials = await GetAsync<UserCredentials>("/api/Traveler/v1/user-credentials");
        return userCredentials;
    }
}
