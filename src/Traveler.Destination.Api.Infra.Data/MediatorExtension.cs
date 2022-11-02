using System.Linq;
using System.Threading.Tasks;
using Traveler.Destination.Api.Domain.SeedWork;
using Traveler.Destination.Api.Infra.Data.Context;
using MediatR;

namespace Traveler.Destination.Api.Infra.Data;

static class MediatorExtension
{
	public static async Task DispatchDomainEventsAsync(this IMediator mediator, ApplicationDbContext ctx)
	{
		var domainEntities = ctx.ChangeTracker
			.Entries<Entity>()
			.Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

		var domainEvents = domainEntities
			.SelectMany(x => x.Entity.DomainEvents)
			.ToList();

		domainEntities.ToList()
			.ForEach(entity => entity.Entity.ClearDomainEvent());

		foreach (var domainEvent in domainEvents)
		{
			await mediator.Publish(domainEvent);
		}
	}
}
