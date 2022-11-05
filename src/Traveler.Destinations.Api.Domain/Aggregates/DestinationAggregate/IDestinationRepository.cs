using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Traveler.Destinations.Api.Domain.SeedWork;

namespace Traveler.Destinations.Api.Domain.Aggregates.DestinationAggregate;

public interface IDestinationRepository : IRepository<Destination>
{
    public Task<ICollection<Destination>> GetAll();
    public Task<Destination> GetById(Guid id);
    public Task<ICollection<Destination>> GetBySearchTerm(string searchTerm);
    Task<ICollection<Destination>> GetByPreferences(int averageSpendIdPreference, IEnumerable<int> destinationPreferences);
}
