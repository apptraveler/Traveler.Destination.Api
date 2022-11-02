using System;
using Traveler.Destination.Api.Infra.CrossCutting.Environments.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Traveler.Destination.Api.Filters.ErrorsModels;

namespace Traveler.Destination.Api.Filters;

public class GlobalExceptionFilterAttribute : Attribute, IExceptionFilter
{
    private readonly ApplicationConfiguration _applicationConfiguration;
    private readonly ILogger<GlobalExceptionFilterAttribute> _logger;

    public GlobalExceptionFilterAttribute(
        ApplicationConfiguration applicationConfiguration,
        ILogger<GlobalExceptionFilterAttribute> logger
    )
    {
        _applicationConfiguration = applicationConfiguration;
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        var eventId = new EventId(188, "GlobalException");

        _logger.LogError(eventId, context.Exception, context.Exception.Message);

        var errorMessage = new DefaultError(false,
            new[]
            {
                new ErrorsResponse(
                    _applicationConfiguration.GlobalErrorCode,
                    _applicationConfiguration.GlobalErrorMessage,
                    context.HttpContext.Request.Path,
                    StatusCodes.Status400BadRequest
                )
            }
        );

        context.Result = new BadRequestObjectResult(errorMessage);
    }
}
