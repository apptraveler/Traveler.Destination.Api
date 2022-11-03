using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Net.Http.Headers;
using Traveler.Destination.Api.Application.Adapters.Identity;

namespace Traveler.Destination.Api.Infra.CrossCutting.IoC.Configurations.Authentication;

public class BearerAuthenticationScheme : AuthenticationHandler<BearerAuthenticationSchemeOptions>
{
    private readonly IAuthorization _authorization;

    public BearerAuthenticationScheme(
        IOptionsMonitor<BearerAuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock, IAuthorization authorization) : base(options, logger, encoder, clock)
    {
        _authorization = authorization;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        try
        {
            string authorization = Request.Headers[HeaderNames.Authorization];

            if (string.IsNullOrEmpty(authorization))
            {
                return AuthenticateResult.Fail("Token inválido");
            }

            var token = authorization["Bearer ".Length..].Trim();
            var tokenHandler = new JwtSecurityTokenHandler();

            if (!tokenHandler.CanReadToken(token))
            {
                return AuthenticateResult.Fail("Token inválido");
            }

            var userCredentials = await _authorization.AuthorizeAsync(token);

            if (userCredentials is null)
            {
                return AuthenticateResult.Fail("Token inválido");
            }

            var claims = new List<Claim>()
            {
                new(UserClaims.UserId, userCredentials.UserId.ToString())
            };

            var ticket = GetAuthorizationTicket(claims);

            return AuthenticateResult.Success(ticket);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private AuthenticationTicket GetAuthorizationTicket(IEnumerable<Claim> claims)
    {
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new System.Security.Principal.GenericPrincipal(identity, null);
        return new AuthenticationTicket(principal, Scheme.Name);
    }
}

public class BearerAuthenticationSchemeOptions : AuthenticationSchemeOptions
{
}
