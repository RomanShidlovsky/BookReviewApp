using System.Net;
using Book.Application.DTOs.Book.RequestDTOs;
using Book.Application.DTOs.Book.ResponseDTOs;
using Book.Application.Features.BookFeatures.Commands.Create;
using Book.Application.Features.BookFeatures.Commands.Delete;
using Book.Application.Features.BookFeatures.Commands.Update;
using Book.Application.Features.BookFeatures.Queries.GetBooks;
using Book.Application.Features.BookFeatures.Queries.GetById;
using Book.Application.Features.BookFeatures.Queries.GetByOpenLibraryKey;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Constants;
using Shared.Wrappers;

namespace Book.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(IMediator _mediator, ILogger<BooksController> _logger) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<BookResponseDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetBooks(CancellationToken cancellationToken, [FromQuery] string filterQueryString = "", 
        [FromQuery] string orderByQueryString = "", [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetBooksQuery(filterQueryString, orderByQueryString, pageNumber, pageSize);
        var result = await _mediator.Send(query, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(BookResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var query = new GetBookByIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }
    
    [HttpGet("{key}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(BookResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetByOpenLibraryKey([FromRoute] string key, CancellationToken cancellationToken)
    {
        var query = new GetBookByOpenLibraryKeyQuery(key);
        var result = await _mediator.Send(query, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }

    [HttpPost]
    [Authorize(Roles = Roles.Admin)]
    [ProducesResponseType(typeof(BookResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(IEnumerable<Error>), (int)HttpStatusCode.UnprocessableEntity)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.Conflict)]
    public async Task<IActionResult> Create([FromBody] CreateBookDto dto, CancellationToken cancellationToken)
    {
        var command = new CreateBookCommand(dto);
        var result = await _mediator.Send(command, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }

    [HttpPut]
    [Authorize(Roles = Roles.Admin)]
    [ProducesResponseType(typeof(BookResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(IEnumerable<Error>), (int)HttpStatusCode.UnprocessableEntity)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.Conflict)]
    public async Task<IActionResult> Update([FromBody] UpdateBookDto dto, CancellationToken cancellationToken)
    {
        var command = new UpdateBookCommand(dto);
        var result = await _mediator.Send(command, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = Roles.Admin)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        var command = new DeleteBookCommand(id);
        var result = await _mediator.Send(command, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }
}