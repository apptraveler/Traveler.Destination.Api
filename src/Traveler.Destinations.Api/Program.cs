using System;
using MediatR;
using Traveler.Destinations.Api.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Traveler.Destinations.Api.Application.CommandHandlers;
using Traveler.Destinations.Api.Infra.CrossCutting.IoC.Configurations;
using Traveler.Destinations.Api.Infra.CrossCutting.IoC.Configurations.Authentication;
using Traveler.Destinations.Api.Infra.CrossCutting.IoC.Configurations.Logging;
using Traveler.Destinations.Api.Infra.CrossCutting.IoC.Configurations.HealthCheck;
using Serilog;
using Traveler.Destinations.Api.Application.Schedulers;

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "6000";
builder.WebHost.UseUrls("http://*:" + port);

builder.Host.UseSerilog();

builder.Services.AddCustomLogging(builder.Configuration);
builder.Services.AddCustomAuthentication();
builder.Services.AddApiVersioning();
builder.Services.AddVersionedApiExplorer();
builder.Services.AddSwaggerSetup();
builder.Services.AddAutoMapper();
builder.Services.AddDependencyInjectionSetup(builder.Configuration);
builder.Services.AddMediatR(typeof(CommandHandler<,>));
builder.Services.AddScoped<GlobalExceptionFilterAttribute>();
builder.Services.AddDatabaseSetup();
builder.Services.AddControllers();
builder.Services.AddHealthCheck(builder.Configuration);
builder.Services.AddHostedService<SeedDestinationsJob>();

var app = builder.Build();

app.UseLoggingMiddlewares();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors(corsBuilder =>
{
    corsBuilder.WithOrigins("*");
    corsBuilder.AllowAnyOrigin();
    corsBuilder.AllowAnyMethod();
    corsBuilder.AllowAnyHeader();
});

app.UseRouting();

app.UseAuthorization();

var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
app.UseSwaggerSetup(apiVersionDescriptionProvider);

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHealthCheck();
});

app.Run();
