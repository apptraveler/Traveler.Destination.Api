using Microsoft.EntityFrameworkCore;
using Traveler.Destinations.Api.Domain.SeedWork;
using Traveler.Destinations.Api.Infra.Data.Context;

namespace Traveler.Destinations.Api.Infra.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IAggregateRoot
{
    protected readonly ApplicationDbContext ApplicationDbContext;
    protected readonly DbSet<TEntity> DbSet;

    public Repository(ApplicationDbContext applicationDbContext)
    {
        ApplicationDbContext = applicationDbContext;
        DbSet = applicationDbContext.Set<TEntity>();
    }

    public void Add(TEntity obj)
    {
        ApplicationDbContext.Add(obj);
    }
}
