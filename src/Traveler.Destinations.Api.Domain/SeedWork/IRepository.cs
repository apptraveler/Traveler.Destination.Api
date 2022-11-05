namespace Traveler.Destinations.Api.Domain.SeedWork;

public interface IRepository<TEntity> where TEntity : IAggregateRoot
{
	void Add(TEntity obj);
}