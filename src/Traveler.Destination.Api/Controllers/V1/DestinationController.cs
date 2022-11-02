using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Traveler.Destination.Api.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Traveler.Destination.Api.Application.Commands;
using Traveler.Destination.Api.Application.Queries;
using Traveler.Destination.Api.Application.Response;
using Traveler.Destination.Api.Dtos;

namespace Traveler.Destination.Api.Controllers.V1;

[ApiVersion("1")]
[ApiController]
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
        // todo: buscar guid do usuário
        var query = new GetRecommendedDestinationsByUserIdQuery(Guid.NewGuid());
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
        // todo: buscar guid do usuário
        var command = new BookmarkDestinationByIdCommand(Guid.NewGuid(), id);
        await _bus.Send(command);
        return Response(NoContent());
    }

    [HttpDelete("{id:guid}/bookmark")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RemoveDestinationBookmarkById(Guid id)
    {
        // todo: buscar guid do usuário
        var command = new DeleteBookmarkedDestinationByIdCommand(Guid.NewGuid(), id);
        await _bus.Send(command);
        return Response(NoContent());
    }
}
