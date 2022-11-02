using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Traveler.Destination.Api.Domain.Aggregates.DestinationAggregate;
using Traveler.Destination.Api.Infra.Data.Context;

namespace Traveler.Destination.Api.Infra.Data.Repositories;

public class DestinationRepository : Repository<Domain.Aggregates.DestinationAggregate.Destination>, IDestinationRepository
{
    public DestinationRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }

    public async Task<ICollection<Domain.Aggregates.DestinationAggregate.Destination>> GetAll()
    {
        return await DbSet.ToListAsync();
    }

    public async Task<Domain.Aggregates.DestinationAggregate.Destination> GetById(Guid id)
    {
        return await DbSet.FindAsync(id);
    }
}
