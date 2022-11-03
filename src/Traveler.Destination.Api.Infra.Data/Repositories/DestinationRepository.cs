using System;
using System.Collections.Generic;
using System.Linq;
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
        return await DbSet
            .Include(x => x.Route)
            .Include(x => x.Images)
            .Include(x => x.Tags)
            .Include(x => x.AverageSpend)
            .Include(x => x.ClimateAverage)
            .ToListAsync();
    }

    public async Task<Domain.Aggregates.DestinationAggregate.Destination> GetById(Guid id)
    {
        return await DbSet
            .Include(x => x.Route)
            .Include(x => x.Images)
            .Include(x => x.Tags)
            .Include(x => x.AverageSpend)
            .Include(x => x.ClimateAverage)
            .FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task<ICollection<Domain.Aggregates.DestinationAggregate.Destination>> GetBySearchTerm(string searchTerm)
    {
        return await DbSet.Include(x => x.Route)
            .Include(x => x.Images)
            .Include(x => x.Tags)
            .Include(x => x.AverageSpend)
            .Include(x => x.ClimateAverage)
            .Where(x => EF.Functions.Like(x.Name, $"%{searchTerm}%") || EF.Functions.Like(x.Description, $"%{searchTerm}%"))
            .ToListAsync();
    }
}
