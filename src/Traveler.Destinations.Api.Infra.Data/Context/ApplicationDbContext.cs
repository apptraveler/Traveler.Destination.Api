﻿using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Traveler.Destinations.Api.Infra.CrossCutting.Environments.Configurations;
using Traveler.Destinations.Api.Infra.Data.Mappings.Database;

namespace Traveler.Destinations.Api.Infra.Data.Context;

public class ApplicationDbContext : DbContext
{
    private readonly IMediator _bus;
    private ApplicationConfiguration _applicationConfiguration;

    public ApplicationDbContext(ApplicationConfiguration applicationConfiguration, IMediator bus)
    {
        _applicationConfiguration = applicationConfiguration;
        _bus = bus ?? throw new ArgumentNullException(nameof(bus));
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ApplicationConfiguration applicationConfiguration, IMediator bus) : base(options)
    {
        _applicationConfiguration = applicationConfiguration;
        _bus = bus ?? throw new ArgumentNullException(nameof(bus));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.EnableSensitiveDataLogging();
        options.UseSqlite(_applicationConfiguration.ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DestinationTagsMap());
        modelBuilder.ApplyConfiguration(new DestinationAverageSpendMap());
        modelBuilder.ApplyConfiguration(new ClimateStatusMap());
        modelBuilder.ApplyConfiguration(new DestinationClimateAverageMap());
        modelBuilder.ApplyConfiguration(new DestinationImageMap());
        modelBuilder.ApplyConfiguration(new DestinationTagListMap());
        modelBuilder.ApplyConfiguration(new RouteCoordinatesMap());
        modelBuilder.ApplyConfiguration(new BookmarkedDestinationMap());
        modelBuilder.ApplyConfiguration(new DestinationMap());
        modelBuilder.ApplyConfiguration(new ReviewMap());
        base.OnModelCreating(modelBuilder);
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        // Dispatch Domain Events collection.
        // Choices:
        // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including
        // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
        // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions.
        // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers.
        await _bus.DispatchDomainEventsAsync(this);

        // After executing this line all the changes (from the Command Handler and Domain Event Handlers)
        // performed through the DbContext will be committed
        return await base.SaveChangesAsync(cancellationToken) > 0;
    }
}
