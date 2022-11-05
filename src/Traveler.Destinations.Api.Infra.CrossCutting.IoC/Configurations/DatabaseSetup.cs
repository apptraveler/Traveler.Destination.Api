using System;
using Traveler.Destinations.Api.Infra.Data.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Traveler.Destinations.Api.Infra.CrossCutting.IoC.Configurations;

public static class DatabaseSetup
{
	public static void AddDatabaseSetup(this IServiceCollection services)
	{
		if (services == null) throw new ArgumentNullException(nameof(services));

		services.AddDbContext<ApplicationDbContext>(ServiceLifetime.Scoped);
	}
}