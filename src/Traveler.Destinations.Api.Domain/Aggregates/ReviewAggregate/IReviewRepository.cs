using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Traveler.Destinations.Api.Domain.SeedWork;

namespace Traveler.Destinations.Api.Domain.Aggregates.ReviewAggregate;

public interface IReviewRepository : IRepository<Review>
{
    public Task<Review> GetById(Guid id);
    public Task<ICollection<Review>> GetByDestinationId(Guid destinationId);
    public Task<ICollection<Review>> GetByUserId(Guid userId);
    public void Delete(Review review);
}
