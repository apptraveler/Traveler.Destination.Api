using System.Threading.Tasks;

namespace Traveler.Destinations.Api.Domain.SeedWork;

public interface IUnitOfWork
{
	Task<bool> CommitAsync();
}
