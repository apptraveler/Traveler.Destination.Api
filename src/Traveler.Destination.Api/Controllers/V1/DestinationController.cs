using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Traveler.Destination.Api.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Traveler.Destination.Api.Application.Commands;
using Traveler.Destination.Api.Application.Queries;
using Traveler.Destination.Api.Application.Response;
using Traveler.Destination.Api.Dtos;
using Traveler.Destination.Api.Infra.CrossCutting.IoC.Configurations.Authentication;

namespace Traveler.Destination.Api.Controllers.V1;

[ApiVersion("1")]
[ApiController]
[Authorize(AuthenticationSchemes = CustomAuthenticationSchemes.Bearer)]
public class DestinationController : BaseController
{
    private readonly IMediator _bus;

    public DestinationController(INotificationHandler<ExceptionNotification> notifications, IMediator bus) : base(notifications)
    {
        _bus = bus;
    }

    [HttpGet]
    [ProducesResponseType(typeof(Response<ICollection<DestinationResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDestinations()
    {
        var query = new GetDestinationsQuery();
        var response = await _bus.Send(query);
        return Response(Ok(new Response<ICollection<DestinationResponse>>(response)));
    }

    [HttpGet("{searchTerm}")]
    [ProducesResponseType(typeof(Response<ICollection<DestinationResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDestinationsBySearchTerm(string searchTerm)
    {
        var query = new GetDestinationsBySearchTermQuery(searchTerm);
        var response = await _bus.Send(query);
        return Response(Ok(new Response<ICollection<DestinationResponse>>(response)));
    }

    [HttpGet("recommendations")]
    [ProducesResponseType(typeof(Response<ICollection<DestinationResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRecommendedDestinations()
    {
        var userId = GetIdentityClaim(UserClaims.UserId);
        var query = new GetRecommendedDestinationsByUserIdQuery(Guid.Parse(userId));
        var response = await _bus.Send(query);
        return Response(Ok(new Response<ICollection<DestinationResponse>>(response)));
    }

    [HttpGet("bookmarked")]
    [ProducesResponseType(typeof(Response<ICollection<DestinationResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBookmarkedDestinations()
    {
        var query = new GetBookmarkedDestinationsQuery(Guid.NewGuid());
        var response = await _bus.Send(query);
        return Response(Ok(new Response<ICollection<DestinationResponse>>(response)));
    }

    [HttpPost("{id:guid}/bookmark")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> BookmarkDestinationById(Guid id)
    {
        var userId = GetIdentityClaim(UserClaims.UserId);
        var command = new BookmarkDestinationByIdCommand(Guid.Parse(userId), id);
        await _bus.Send(command);
        return Response(NoContent());
    }

    [HttpDelete("{id:guid}/bookmark")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RemoveDestinationBookmarkById(Guid id)
    {
        var userId = GetIdentityClaim(UserClaims.UserId);
        var command = new DeleteBookmarkedDestinationByIdCommand(Guid.Parse(userId), id);
        await _bus.Send(command);
        return Response(NoContent());
    }
}
