using System.Threading.Tasks;

namespace Traveler.Destinations.Api.Application.Adapters.Identity;

public interface IAuthorization
{
    public Task<UserCredentials> AuthorizeAsync(string token);
}
