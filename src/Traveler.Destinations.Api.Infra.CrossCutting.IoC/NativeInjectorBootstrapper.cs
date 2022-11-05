using System;
using Traveler.Destinations.Api.Application.Behaviors;
using Traveler.Destinations.Api.Infra.CrossCutting.Environments.Configurations;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Traveler.Destinations.Api.Application.Adapters.Identity;
using Traveler.Destinations.Api.Domain.Aggregates.BookmarkedDestination;
using Traveler.Destinations.Api.Domain.Aggregates.DestinationAggregate;
using Traveler.Destinations.Api.Domain.Exceptions;
using Traveler.Destinations.Api.Domain.SeedWork;
using Traveler.Destinations.Api.Infra.Data.Repositories;
using Traveler.Destinations.Api.Infra.Data.UnitOfWork;
using Traveler.Destinations.Api.Infra.Proxy.Identity.Core;

namespace Traveler.Destinations.Api.Infra.CrossCutting.IoC;

public static class NativeInjectorBootstrapper
{
    public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        RegisterData(services);
        RegisterMediatR(services);
        RegisterEnvironments(services, configuration);
        RegisterProxies(services);
    }

    private static void RegisterData(IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IDestinationRepository, DestinationRepository>();
        services.AddScoped<IBookmarkDestinationRepository, BookmarkedDestinationRepository>();
    }

    private static void RegisterMediatR(IServiceCollection services)
    {
        const string applicationAssemblyName = "Traveler.Destinations.Api.Application"; // use your project name
        var assembly = AppDomain.CurrentDomain.Load(applicationAssemblyName);

        AssemblyScanner
            .FindValidatorsInAssembly(assembly)
            .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

        // injection for Mediator
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(PipelineBehavior<,>));
        services.AddScoped<INotificationHandler<ExceptionNotification>, ExceptionNotificationHandler>();
    }

    private static void RegisterEnvironments(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration.GetSection(nameof(ApplicationConfiguration)).Get<ApplicationConfiguration>());
        services.AddSingleton(configuration.GetSection(nameof(IdentityConfiguration)).Get<IdentityConfiguration>());
    }

    private static void RegisterProxies(IServiceCollection services)
    {
        services.AddHttpClient<IAuthorization, IdentityService>();
    }
}
