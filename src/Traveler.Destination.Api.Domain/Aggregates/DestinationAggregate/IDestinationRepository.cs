using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Traveler.Destination.Api.Domain.SeedWork;

namespace Traveler.Destination.Api.Domain.Aggregates.DestinationAggregate;

public interface IDestinationRepository : IRepository<Destination>
{
    public Task<ICollection<Destination>> GetAll();
    public Task<Destination> GetById(Guid id);
    public Task<ICollection<Destination>> GetBySearchTerm(string searchTerm);
}
