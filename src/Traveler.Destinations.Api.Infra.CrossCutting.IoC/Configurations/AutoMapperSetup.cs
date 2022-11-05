using System;
using Traveler.Destinations.Api.Application.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace Traveler.Destinations.Api.Infra.CrossCutting.IoC.Configurations;

public static class AutoMapperSetup
{
	public static void AddAutoMapper(this IServiceCollection services)
	{
		if (services == null) throw new ArgumentNullException(nameof(services));

		services.AddAutoMapper(typeof(MappingProfile));
	}
}