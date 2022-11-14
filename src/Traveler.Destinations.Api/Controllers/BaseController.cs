using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Traveler.Destinations.Api.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Traveler.Destinations.Api.Dtos;
using Traveler.Destinations.Api.Filters;

namespace Traveler.Destinations.Api.Controllers;

[Route("traveler/[controller]/v{version:apiVersion}")]
[ServiceFilter(typeof(GlobalExceptionFilterAttribute))]
public class BaseController : Controller
{
    private readonly ExceptionNotificationHandler _notifications;

    protected IEnumerable<ExceptionNotification> Notifications => _notifications.GetNotifications();

    protected BaseController(INotificationHandler<ExceptionNotification> notifications)
    {
        _notifications = (ExceptionNotificationHandler) notifications;
    }

    protected bool IsValidOperation()
    {
        return !_notifications.HasNotifications();
    }

    protected new IActionResult CreateResponse(IActionResult action)
    {
        if (!IsValidOperation())
        {
            return BadRequest(new Response<object>(
                _notifications.GetNotifications())
            );
        }

        return action;
    }

    protected string GetIdentityClaim(string claimName)
    {
        var identity = (ClaimsIdentity) User.Identity!;
        var claim = identity.Claims.FirstOrDefault(claim => claim.Type.Equals(claimName, StringComparison.CurrentCultureIgnoreCase));

        if (string.IsNullOrEmpty(claim?.Value))
        {
            Response.StatusCode = StatusCodes.Status403Forbidden;
            Response.CompleteAsync().ConfigureAwait(false);
            throw new ArgumentNullException($"Não foi encontrado a claim {claimName} do usuários");
        }

        return claim.Value;
    }
}
