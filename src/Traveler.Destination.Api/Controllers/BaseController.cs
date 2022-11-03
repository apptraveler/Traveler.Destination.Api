using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Traveler.Destination.Api.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Traveler.Destination.Api.Application.Response;
using Traveler.Destination.Api.Dtos;
using Traveler.Destination.Api.Filters;

namespace Traveler.Destination.Api.Controllers;

[Route("traveler/[controller]/v{version:apiVersion}")]
[ServiceFilter(typeof(GlobalExceptionFilterAttribute))]
public class BaseController : Controller
{
    private readonly ExceptionNotificationHandler _notifications;

    protected IEnumerable<ExceptionNotification> Notifications => _notifications.GetNotifications();

    protected BaseController(INotificationHandler<ExceptionNotification> notifications)
    {
        _notifications = (ExceptionNotificationHandler)notifications;
    }

    protected bool IsValidOperation()
    {
        return !_notifications.HasNotifications();
    }

    protected new IActionResult Response(IActionResult action)
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
        return claim?.Value ?? string.Empty;
    }
}
