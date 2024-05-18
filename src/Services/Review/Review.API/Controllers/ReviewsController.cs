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
using Review.Application.Features.ReviewFeatures.Commands.Dislike;
using Review.Application.Features.ReviewFeatures.Commands.Like;
using Review.Application.Features.ReviewFeatures.Commands.Undislike;
using Review.Application.Features.ReviewFeatures.Commands.Unlike;
using Review.Application.Features.ReviewFeatures.Commands.Update;
using Review.Application.Features.ReviewFeatures.Queries.GetAll;
using Review.Application.Features.ReviewFeatures.Queries.GetBookReviews;
using Review.Application.Features.ReviewFeatures.Queries.GetById;
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
    public async Task<IActionResult> GetAllReviews(CancellationToken cancellationToken)
    {
        var query = new GetAllReviewsQuery();
        var result = await _mediator.Send(query, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }
    
    [HttpGet("book/{bookId:int}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<ReviewResponseDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetBookReviews([FromRoute] int bookId,CancellationToken cancellationToken)
    {
        var query = new GetBookReviewsQuery(bookId);
        var result = await _mediator.Send(query, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }
    
    [HttpGet("{id}")]
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
    
    [HttpPut("{id}")]
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
    
    [HttpDelete("{id}")]
    [Authorize(Roles = $"{Roles.Client}, {Roles.Admin}, {Roles.Reviewer}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> DeleteReview([FromRoute] string id, CancellationToken cancellationToken)
    {
        var command = new DeleteReviewCommand(id);
        var result = await _mediator.Send(command, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }
    
    [HttpPut("{id}/comments")]
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
    
    [HttpPut("{id}/likes")]
    [Authorize(Roles = $"{Roles.Client}, {Roles.Admin}, {Roles.Reviewer}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Like([FromBody] LikeReviewDto dto, CancellationToken cancellationToken)
    {
        var command = new LikeReviewCommand(dto);
        var result = await _mediator.Send(command, cancellationToken);
        
        return ApiResponse.GetObjectResult(result, _logger);
    }
    
    [HttpDelete("{id}/likes")]
    [Authorize(Roles = $"{Roles.Client}, {Roles.Admin}, {Roles.Reviewer}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Unlike([FromBody] LikeReviewDto dto, CancellationToken cancellationToken)
    {
        var command = new UnlikeReviewCommand(dto);
        var result = await _mediator.Send(command, cancellationToken);
        
        return ApiResponse.GetObjectResult(result, _logger);
    }

    [HttpPut("{id}/dislikes")]
    [Authorize(Roles = $"{Roles.Client}, {Roles.Admin}, {Roles.Reviewer}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Dislike([FromBody] DislikeReviewDto dto, CancellationToken cancellationToken)
    {
        var command = new DislikeReviewCommand(dto);
        var result = await _mediator.Send(command, cancellationToken);
        
        return ApiResponse.GetObjectResult(result, _logger);
    }
    
    [HttpDelete("{id}/dislikes")]
    [Authorize(Roles = $"{Roles.Client}, {Roles.Admin}, {Roles.Reviewer}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Undislike([FromBody] DislikeReviewDto dto, CancellationToken cancellationToken)
    {
        var command = new UndislikeCommand(dto);
        var result = await _mediator.Send(command, cancellationToken);
        
        return ApiResponse.GetObjectResult(result, _logger);
    }
}