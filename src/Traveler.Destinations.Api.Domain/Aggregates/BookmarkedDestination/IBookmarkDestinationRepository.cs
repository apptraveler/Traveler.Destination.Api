using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Traveler.Destinations.Api.Domain.SeedWork;

namespace Traveler.Destinations.Api.Domain.Aggregates.BookmarkedDestination;

public interface IBookmarkDestinationRepository : IRepository<BookmarkedDestination>
{
    public Task<BookmarkedDestination> GetSpecificDestinationByUserId(Guid destinationId, Guid userId);
    public Task<ICollection<BookmarkedDestination>> GetAllByUserId(Guid userId);
    public void Remove(BookmarkedDestination bookmarkedDestination);
}
