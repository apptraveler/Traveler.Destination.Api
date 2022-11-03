using System.Threading.Tasks;

namespace Traveler.Destination.Api.Application.Adapters.Identity;

public interface IAuthorization
{
    public Task<UserCredentials> AuthorizeAsync(string token);
}
