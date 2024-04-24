using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Review.Application.DTOs.RequestDTOs;
using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Features.BookFeatures.Commands.Update;
using Review.Application.Features.ReviewFeatures.Commands.AddComment;
using Review.Application.Features.ReviewFeatures.Commands.Create;
using Review.Application.Features.ReviewFeatures.Commands.Delete;
using Review.Application.Features.ReviewFeatures.Commands.Update;
using Review.Application.Features.ReviewFeatures.Queries.GetById;
using Review.Application.Features.ReviewFeatures.Queries.GetPaged;
using Shared;
using Shared.Constants;
using Shared.Wrappers;

namespace Review.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController(IMediator _mediator, ILogger<ReviewsController> _logger) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<ReviewResponseDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetPagedReviews(CancellationToken cancellationToken, [FromQuery] string filterQueryString = "", 
        [FromQuery] string orderByQueryString = "", [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetPagedReviewsQuery(filterQueryString, orderByQueryString, pageNumber, pageSize);
        var result = await _mediator.Send(query, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }
    
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ReviewResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetReviewById([FromRoute] string id, CancellationToken cancellationToken)
    {
        var query = new GetReviewByIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }
    
    [HttpPost]
    [Authorize(Roles = $"{Roles.Client}, {Roles.Admin}, {Roles.Reviewer}")]
    [ProducesResponseType(typeof(ReviewResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(IEnumerable<Error>), (int)HttpStatusCode.UnprocessableEntity)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.Conflict)]
    public async Task<IActionResult> CreateReview([FromBody] CreateReviewDto dto, CancellationToken cancellationToken)
    {
        var command = new CreateReviewCommand(dto);
        var result = await _mediator.Send(command, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }
    
    [HttpPut("{id:guid}")]
    [Authorize(Roles = $"{Roles.Client}, {Roles.Admin}, {Roles.Reviewer}")]
    [ProducesResponseType(typeof(ReviewResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(IEnumerable<Error>), (int)HttpStatusCode.UnprocessableEntity)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.Conflict)]
    public async Task<IActionResult> UpdateReview([FromBody] UpdateReviewDto dto, CancellationToken cancellationToken)
    {
        var command = new UpdateReviewCommand(dto);
        var result = await _mediator.Send(command, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }
    
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = $"{Roles.Client}, {Roles.Admin}, {Roles.Reviewer}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> DeleteReview([FromRoute] string id, CancellationToken cancellationToken)
    {
        var command = new DeleteReviewCommand(id);
        var result = await _mediator.Send(command, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }
    
    [HttpPut("{id:guid}/comments")]
    [Authorize(Roles = $"{Roles.Client}, {Roles.Admin}, {Roles.Reviewer}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> AddAuthorToBook([FromBody] AddCommentToReviewDto dto, CancellationToken cancellationToken)
    {
        var command = new AddCommentToReviewCommand(dto);
        var result = await _mediator.Send(command, cancellationToken);
        
        return ApiResponse.GetObjectResult(result, _logger);
    }
}