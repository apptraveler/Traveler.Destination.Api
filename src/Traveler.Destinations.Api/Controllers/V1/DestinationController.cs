using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Traveler.Destinations.Api.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Traveler.Destinations.Api.Application.Commands;
using Traveler.Destinations.Api.Application.Queries;
using Traveler.Destinations.Api.Application.Response;
using Traveler.Destinations.Api.Dtos;
using Traveler.Destinations.Api.Dtos.Request;
using Traveler.Destinations.Api.Infra.CrossCutting.IoC.Configurations.Authentication;

namespace Traveler.Destinations.Api.Controllers.V1;

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
        var destinations = await _bus.Send(query);

        if (destinations is null || !destinations.Any()) return CreateResponse(Ok(new Response<ICollection<DestinationResponse>>(destinations)));

        var userId = GetIdentityClaim(UserClaims.UserId);
        var queryBookmark = new GetBookmarkedDestinationsQuery(Guid.Parse(userId));
        var bookmarkedDestinations = await _bus.Send(queryBookmark);

        foreach (var destination in destinations)
        {
            destination.Bookmarked = bookmarkedDestinations.Any(x => x.Name.Equals(destination.Name));
        }

        return CreateResponse(Ok(new Response<ICollection<DestinationResponse>>(destinations)));
    }

    [HttpGet("{searchTerm}")]
    [ProducesResponseType(typeof(Response<ICollection<DestinationResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDestinationsBySearchTerm(string searchTerm)
    {
        var query = new GetDestinationsBySearchTermQuery(searchTerm);
        var destinations = await _bus.Send(query);

        if (destinations is null || !destinations.Any()) return CreateResponse(Ok(new Response<ICollection<DestinationResponse>>(destinations)));

        var userId = GetIdentityClaim(UserClaims.UserId);
        var queryBookmark = new GetBookmarkedDestinationsQuery(Guid.Parse(userId));
        var bookmarkedDestinations = await _bus.Send(queryBookmark);

        foreach (var destination in destinations)
        {
            destination.Bookmarked = bookmarkedDestinations.Any(x => x.Name.Equals(destination.Name));
        }

        return CreateResponse(Ok(new Response<ICollection<DestinationResponse>>(destinations)));
    }

    [HttpGet("recommendations")]
    [ProducesResponseType(typeof(Response<ICollection<DestinationResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRecommendedDestinations()
    {
        var userId = GetIdentityClaim(UserClaims.UserId);
        var averageSpendPreferenceId = GetIdentityClaim(UserClaims.AverageSpendPreference);
        var locationTagsIds = GetIdentityClaim(UserClaims.LocationTags)
            .Split(",")
            .Select(x => Convert.ToInt32(x))
            .ToList();

        var query = new GetRecommendedDestinationsByUserIdQuery(Guid.Parse(userId), locationTagsIds, Convert.ToInt32(averageSpendPreferenceId));
        var destinations = await _bus.Send(query);

        if (destinations is null || !destinations.Any()) return CreateResponse(Ok(new Response<ICollection<DestinationResponse>>(destinations)));

        var queryBookmark = new GetBookmarkedDestinationsQuery(Guid.Parse(userId));
        var bookmarkedDestinations = await _bus.Send(queryBookmark);

        foreach (var destination in destinations)
        {
            destination.Bookmarked = bookmarkedDestinations.Any(x => x.Name.Equals(destination.Name));
        }

        return CreateResponse(Ok(new Response<ICollection<DestinationResponse>>(destinations)));
    }

    [HttpGet("bookmarked")]
    [ProducesResponseType(typeof(Response<ICollection<DestinationResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBookmarkedDestinations()
    {
        var userId = GetIdentityClaim(UserClaims.UserId);
        var query = new GetBookmarkedDestinationsQuery(Guid.Parse(userId));
        var response = await _bus.Send(query);

        foreach (var destination in response)
        {
            destination.Bookmarked = true;
        }

        return CreateResponse(Ok(new Response<ICollection<DestinationResponse>>(response)));
    }

    [HttpPost("{id:guid}/bookmark")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> BookmarkDestinationById(Guid id)
    {
        var userId = GetIdentityClaim(UserClaims.UserId);
        var command = new BookmarkDestinationByIdCommand(Guid.Parse(userId), id);
        await _bus.Send(command);
        return CreateResponse(NoContent());
    }

    [HttpDelete("{id:guid}/bookmark")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RemoveDestinationBookmarkById(Guid id)
    {
        var userId = GetIdentityClaim(UserClaims.UserId);
        var command = new DeleteBookmarkedDestinationByIdCommand(Guid.Parse(userId), id);
        await _bus.Send(command);
        return CreateResponse(NoContent());
    }

    [HttpGet("{id:guid}/reviews")]
    [ProducesResponseType(typeof(Response<ICollection<ReviewResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetReviews([FromRoute] Guid id)
    {
        var command = new GetReviewsByDestinationIdQuery(id);
        var response = await _bus.Send(command);
        return CreateResponse(Ok(new Response<ICollection<ReviewResponse>>(response)));
    }

    [HttpPost("{id:guid}/review")]
    [ProducesResponseType(typeof(Response<ReviewResponse>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateReview([FromRoute] Guid id, [FromBody] CreateReviewRequest createReviewRequest)
    {
        var userId = GetIdentityClaim(UserClaims.UserId);
        var fullName = GetIdentityClaim(UserClaims.FullName);
        var command = new CreateReviewByDestinationIdCommand(id, Guid.Parse(userId), fullName, createReviewRequest.Message, createReviewRequest.Rate);
        var response = await _bus.Send(command);
        return CreateResponse(Created(Request.Path.ToUriComponent(), new Response<ReviewResponse>(response)));
    }

    [HttpDelete("review/{reviewId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteReview([FromRoute] Guid reviewId)
    {
        var userId = GetIdentityClaim(UserClaims.UserId);
        var command = new DeleteReviewByIdCommand(Guid.Parse(userId), reviewId);
        await _bus.Send(command);
        return CreateResponse(NoContent());
    }
}
