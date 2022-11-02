using System;
using Traveler.Destination.Api.Application.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace Traveler.Destination.Api.Infra.CrossCutting.IoC.Configurations;

public static class AutoMapperSetup
{
	public static void AddAutoMapper(this IServiceCollection services)
	{
		if (services == null) throw new ArgumentNullException(nameof(services));

		services.AddAutoMapper(typeof(MappingProfile));
	}
}