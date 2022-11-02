using System.Threading.Tasks;

namespace Traveler.Destination.Api.Domain.SeedWork;

public interface IUnitOfWork
{
	Task<bool> CommitAsync();
}
