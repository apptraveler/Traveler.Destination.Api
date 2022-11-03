using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Traveler.Destination.Api.Domain.Aggregates.BookmarkedDestination;
using Traveler.Destination.Api.Infra.Data.Context;

namespace Traveler.Destination.Api.Infra.Data.Repositories;

public class BookmarkedDestinationRepository : Repository<BookmarkedDestination>, IBookmarkDestinationRepository
{
    public BookmarkedDestinationRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }

    public async Task<BookmarkedDestination> GetSpecificDestinationByUserId(Guid destinationId, Guid userId)
    {
        return await DbSet.FirstOrDefaultAsync(x => x.DestinationId.Equals(destinationId) && x.UserId.Equals(userId));
    }

    public async Task<ICollection<BookmarkedDestination>> GetAllByUserId(Guid userId)
    {
        return await DbSet
            .Where(x => x.UserId.Equals(userId))
            .ToListAsync();
    }

    public void Remove(BookmarkedDestination bookmarkedDestination)
    {
        DbSet.Remove(bookmarkedDestination);
    }
}
