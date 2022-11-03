using System;
using Microsoft.Extensions.DependencyInjection;

namespace Traveler.Destination.Api.Infra.CrossCutting.IoC.Configurations.Authentication;

public static class CustomAuthenticationSetup
{
    public static void AddCustomAuthentication(this IServiceCollection services)
    {
        if (services is null) throw new ArgumentNullException(nameof(services));

        services.AddAuthentication()
            .AddScheme<BearerAuthenticationSchemeOptions, BearerAuthenticationScheme>(CustomAuthenticationSchemes.Bearer, null);
    }
}
