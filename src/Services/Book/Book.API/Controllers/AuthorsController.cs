using System.Net;
using Book.Application.DTOs.Author.RequestDTOs;
using Book.Application.DTOs.Author.ResponseDTOs;
using Book.Application.Features.AuthorFeatures.Commands.Create;
using Book.Application.Features.AuthorFeatures.Commands.Delete;
using Book.Application.Features.AuthorFeatures.Commands.Update;
using Book.Application.Features.AuthorFeatures.Commands.UploadImage;
using Book.Application.Features.AuthorFeatures.Queries.GetAll;
using Book.Application.Features.AuthorFeatures.Queries.GetById;
using Book.Application.Features.AuthorFeatures.Queries.GetByOpenLibraryKey;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Constants;
using Shared.Wrappers;

namespace Book.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController(IMediator _mediator, ILogger<AuthorsController> _logger) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<AuthorResponseDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetAllAuthors(CancellationToken cancellationToken)
    {
        var query = new GetAllAuthorsQuery();
        var result = await _mediator.Send(query, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthorResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetAuthorById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var query = new GetAuthorByIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }
    
    [HttpGet("{key}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthorResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetAuthorByOpenLibraryKey([FromRoute] string key, CancellationToken cancellationToken)
    {
        var query = new GetAuthorByOpenLibraryKeyQuery(key);
        var result = await _mediator.Send(query, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }

    [HttpPost]
    [Authorize(Roles = Roles.Admin)]
    [ProducesResponseType(typeof(AuthorResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(IEnumerable<Error>), (int)HttpStatusCode.UnprocessableEntity)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.Conflict)]
    public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorDto dto, CancellationToken cancellationToken)
    {
        var command = new CreateAuthorCommand(dto);
        var result = await _mediator.Send(command, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = Roles.Admin)]
    [ProducesResponseType(typeof(AuthorResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(IEnumerable<Error>), (int)HttpStatusCode.UnprocessableEntity)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.Conflict)]
    public async Task<IActionResult> UpdateAuthor([FromBody] UpdateAuthorDto dto, CancellationToken cancellationToken)
    {
        var command = new UpdateAuthorCommand(dto);
        var result = await _mediator.Send(command, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }
    
    [HttpDelete("{id:int}")]
    [Authorize(Roles = Roles.Admin)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> DeleteAuthor([FromRoute] int id, CancellationToken cancellationToken)
    {
        var command = new DeleteAuthorCommand(id);
        var result = await _mediator.Send(command, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }

    [HttpPost("{id:int}/image"), DisableRequestSizeLimit]
    [Authorize(Roles = Roles.Admin)]
    [ProducesResponseType(typeof(string),(int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> UploadImage([FromRoute] int id, CancellationToken cancellationToken)
    {
        var formCollection = await Request.ReadFormAsync(cancellationToken);
        var files = formCollection.Files;
        
        if (!files.Any())
            return BadRequest("No files found in the request");

        if (files.Count > 1)
            return BadRequest("Cannot upload more than one file at a time");

        if (files[0].Length <= 0)
            return BadRequest("Invalid file length, seems to be empty");
        
        var command = new UploadAuthorImageCommand(id, files[0]);
        var result = await _mediator.Send(command, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }
}