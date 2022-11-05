using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Traveler.Destinations.Api.Domain.Aggregates.DestinationAggregate;
using Traveler.Destinations.Api.Infra.Data.Context;

namespace Traveler.Destinations.Api.Infra.Data.Repositories;

public class DestinationRepository : Repository<Destination>, IDestinationRepository
{
    public DestinationRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }

    public async Task<ICollection<Destination>> GetAll()
    {
        return await DbSet
            .Include(x => x.Route)
            .Include(x => x.Images)
            .Include(x => x.Tags)
            .Include(x => x.AverageSpend)
            .Include(x => x.ClimateAverage)
            .ToListAsync();
    }

    public async Task<Destination> GetById(Guid id)
    {
        return await DbSet
            .Include(x => x.Route)
            .Include(x => x.Images)
            .Include(x => x.Tags)
            .Include(x => x.AverageSpend)
            .Include(x => x.ClimateAverage)
            .FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task<ICollection<Destination>> GetBySearchTerm(string searchTerm)
    {
        return await DbSet.Include(x => x.Route)
            .Include(x => x.Images)
            .Include(x => x.Tags)
            .Include(x => x.AverageSpend)
            .Include(x => x.ClimateAverage)
            .Where(x => EF.Functions.Like(x.Name, $"%{searchTerm}%") || EF.Functions.Like(x.Description, $"%{searchTerm}%"))
            .ToListAsync();
    }

    public async Task<ICollection<Destination>> GetByPreferences(int averageSpendIdPreference, IEnumerable<int> destinationPreferencesIds)
    {
        return await DbSet.Include(x => x.Route)
            .Include(x => x.Images)
            .Include(x => x.Tags)
            .Include(x => x.AverageSpend)
            .Include(x => x.ClimateAverage)
            .Where(x => x.Tags.Any(c => destinationPreferencesIds.Contains(c.TagId)) || x.AverageSpend.Id.Equals(averageSpendIdPreference))
            .ToListAsync();
    }
}
