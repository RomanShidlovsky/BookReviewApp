using System.Net;
using Book.Application.DTOs.Language.RequestDTOs;
using Book.Application.DTOs.Language.ResponseDTOs;
using Book.Application.Features.LanguageFeatures.Commands.AddLanguageToBook;
using Book.Application.Features.LanguageFeatures.Commands.Create;
using Book.Application.Features.LanguageFeatures.Commands.Delete;
using Book.Application.Features.LanguageFeatures.Commands.RemoveLanguageFromBook;
using Book.Application.Features.LanguageFeatures.Commands.Update;
using Book.Application.Features.LanguageFeatures.Queries.GetAll;
using Book.Application.Features.LanguageFeatures.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Constants;
using Shared.Wrappers;

namespace Book.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LanguagesController(IMediator _mediator, ILogger<LanguagesController> _logger) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<LanguageResponseDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllLanguagesQuery();
        var result = await _mediator.Send(query, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(LanguageResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var query = new GetLanguageByIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }
    
    [HttpPost]
    [Authorize(Roles = Roles.Admin)]
    [ProducesResponseType(typeof(LanguageResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(IEnumerable<Error>), (int)HttpStatusCode.UnprocessableEntity)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.Conflict)]
    public async Task<IActionResult> Create([FromBody] CreateLanguageDto dto, CancellationToken cancellationToken)
    {
        var command = new CreateLanguageCommand(dto);
        var result = await _mediator.Send(command, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }

    [HttpPut]
    [Authorize(Roles = Roles.Admin)]
    [ProducesResponseType(typeof(LanguageResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(IEnumerable<Error>), (int)HttpStatusCode.UnprocessableEntity)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.Conflict)]
    public async Task<IActionResult> Update([FromBody] UpdateLanguageDto dto, CancellationToken cancellationToken)
    {
        var command = new UpdateLanguageCommand(dto);
        var result = await _mediator.Send(command, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }

    [HttpPut(nameof(AddToBook))]
    [Authorize(Roles = Roles.Admin)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> AddToBook([FromBody] AddLanguageToBookDto dto, CancellationToken cancellationToken)
    {
        var command = new AddLanguageToBookCommand(dto);
        var result = await _mediator.Send(command, cancellationToken);
        
        return ApiResponse.GetObjectResult(result, _logger);
    }
    
    [HttpPut(nameof(RemoveFromBook))]
    [Authorize(Roles = Roles.Admin)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> RemoveFromBook([FromBody] RemoveLanguageFromBookDto dto, CancellationToken cancellationToken)
    {
        var command = new RemoveLanguageFromBookCommand(dto);
        var result = await _mediator.Send(command, cancellationToken);
        
        return ApiResponse.GetObjectResult(result, _logger);
    }
    
    [HttpDelete("{id:int}")]
    [Authorize(Roles = Roles.Admin)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        var command = new DeleteLanguageCommand(id);
        var result = await _mediator.Send(command, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }
}