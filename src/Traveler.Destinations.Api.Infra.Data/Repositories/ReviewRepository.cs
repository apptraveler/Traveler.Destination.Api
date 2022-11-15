using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Traveler.Destinations.Api.Domain.Aggregates.ReviewAggregate;
using Traveler.Destinations.Api.Infra.Data.Context;

namespace Traveler.Destinations.Api.Infra.Data.Repositories;

public class ReviewRepository : Repository<Review>, IReviewRepository
{
    public ReviewRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }

    public async Task<Review> GetById(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task<ICollection<Review>> GetByDestinationId(Guid destinationId)
    {
        return await DbSet
            .Where(review => review.DestinationId.Equals(destinationId))
            .ToListAsync();
    }

    public async Task<ICollection<Review>> GetByUserId(Guid userId)
    {
        return await DbSet
            .Where(review => review.UserId.Equals(userId))
            .ToListAsync();
    }

    public void Delete(Review review)
    {
        DbSet.Remove(review);
    }
}
