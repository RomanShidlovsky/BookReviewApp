using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Review.Application.DTOs.RequestDTOs;
using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Features.CriticReviewFeatures.Commands.AddComment;
using Review.Application.Features.CriticReviewFeatures.Commands.Create;
using Review.Application.Features.CriticReviewFeatures.Commands.Delete;
using Review.Application.Features.CriticReviewFeatures.Commands.Dislike;
using Review.Application.Features.CriticReviewFeatures.Commands.Like;
using Review.Application.Features.CriticReviewFeatures.Commands.Undislike;
using Review.Application.Features.CriticReviewFeatures.Commands.Unlike;
using Review.Application.Features.CriticReviewFeatures.Commands.Update;
using Review.Application.Features.CriticReviewFeatures.Queries.GetAll;
using Review.Application.Features.CriticReviewFeatures.Queries.GetBookReviews;
using Review.Application.Features.CriticReviewFeatures.Queries.GetById;
using Review.Application.Features.CriticReviewFeatures.Queries.GetUserReviews;
using Shared;
using Shared.Constants;
using Shared.Wrappers;

namespace Review.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CriticReviewsController(IMediator _mediator, ILogger<ReviewsController> _logger) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<ReviewResponseDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetAllReviews(CancellationToken cancellationToken)
    {
        var query = new GetAllCriticReviewsQuery();
        var result = await _mediator.Send(query, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }
    
    [HttpGet("book/{bookId:int}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<ReviewResponseDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetBookReviews([FromRoute] int bookId,CancellationToken cancellationToken)
    {
        var query = new GetBookCriticReviewsQuery(bookId);
        var result = await _mediator.Send(query, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }
    
    [HttpGet("user/{userId:int}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<ReviewResponseDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetUserReviews([FromRoute] int userId, CancellationToken cancellationToken)
    {
        var query = new GetUserCriticReviewsQuery(userId);
        var result = await _mediator.Send(query, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }
    
    [HttpGet("{id}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ReviewResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetReviewById([FromRoute] string id, CancellationToken cancellationToken)
    {
        var query = new GetCriticReviewByIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }
    
    [HttpPost]
    [Authorize(Roles = $"{Roles.Reviewer}")]
    [ProducesResponseType(typeof(ReviewResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(IEnumerable<Error>), (int)HttpStatusCode.UnprocessableEntity)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.Conflict)]
    public async Task<IActionResult> CreateReview([FromBody] CreateReviewDto dto, CancellationToken cancellationToken)
    {
        var command = new CreateCriticReviewCommand(dto);
        var result = await _mediator.Send(command, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }
    
    [HttpPut("{id}")]
    [Authorize(Roles = $"{Roles.Reviewer}")]
    [ProducesResponseType(typeof(ReviewResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(IEnumerable<Error>), (int)HttpStatusCode.UnprocessableEntity)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.Conflict)]
    public async Task<IActionResult> UpdateReview([FromBody] UpdateReviewDto dto, CancellationToken cancellationToken)
    {
        var command = new UpdateCriticReviewCommand(dto);
        var result = await _mediator.Send(command, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles = $"{Roles.Reviewer}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> DeleteReview([FromRoute] string id, CancellationToken cancellationToken)
    {
        var command = new DeleteCriticReviewCommand(id);
        var result = await _mediator.Send(command, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }
    
    [HttpPut("{id}/comments")]
    [Authorize(Roles = $"{Roles.Reviewer}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> AddAuthorToBook([FromBody] AddCommentToReviewDto dto, CancellationToken cancellationToken)
    {
        var command = new AddCommentToCriticReviewCommand(dto);
        var result = await _mediator.Send(command, cancellationToken);
        
        return ApiResponse.GetObjectResult(result, _logger);
    }
    
    [HttpPut("{id}/likes")]
    [Authorize(Roles = $"{Roles.Reviewer}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Like([FromBody] LikeReviewDto dto, CancellationToken cancellationToken)
    {
        var command = new LikeCriticReviewCommand(dto);
        var result = await _mediator.Send(command, cancellationToken);
        
        return ApiResponse.GetObjectResult(result, _logger);
    }
    
    [HttpDelete("{id}/likes")]
    [Authorize(Roles = $"{Roles.Reviewer}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Unlike([FromBody] LikeReviewDto dto, CancellationToken cancellationToken)
    {
        var command = new UnlikeCriticReviewCommand(dto);
        var result = await _mediator.Send(command, cancellationToken);
        
        return ApiResponse.GetObjectResult(result, _logger);
    }

    [HttpPut("{id}/dislikes")]
    [Authorize(Roles = $"{Roles.Reviewer}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Dislike([FromBody] DislikeReviewDto dto, CancellationToken cancellationToken)
    {
        var command = new DislikeCriticReviewCommand(dto);
        var result = await _mediator.Send(command, cancellationToken);
        
        return ApiResponse.GetObjectResult(result, _logger);
    }
    
    [HttpDelete("{id}/dislikes")]
    [Authorize(Roles = $"{Roles.Reviewer}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Undislike([FromBody] DislikeReviewDto dto, CancellationToken cancellationToken)
    {
        var command = new UndislikeCriticReviewCommand(dto);
        var result = await _mediator.Send(command, cancellationToken);
        
        return ApiResponse.GetObjectResult(result, _logger);
    }
}