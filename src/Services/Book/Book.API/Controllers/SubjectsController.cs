using System.Net;
using Book.Application.DTOs.Subject.RequestDTOs;
using Book.Application.DTOs.Subject.ResponseDTOs;
using Book.Application.Features.SubjectFeatures.Commands.AddSubjectToBook;
using Book.Application.Features.SubjectFeatures.Commands.Create;
using Book.Application.Features.SubjectFeatures.Commands.Delete;
using Book.Application.Features.SubjectFeatures.Commands.RemoveSubjectFromBook;
using Book.Application.Features.SubjectFeatures.Commands.Update;
using Book.Application.Features.SubjectFeatures.Queries.GetAll;
using Book.Application.Features.SubjectFeatures.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Constants;
using Shared.Wrappers;

namespace Book.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubjectsController(IMediator _mediator, ILogger<SubjectsController> _logger) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<SubjectResponseDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetAllSubjects(CancellationToken cancellationToken)
    {
        var query = new GetAllSubjectsQuery();
        var result = await _mediator.Send(query, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(SubjectResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetSubjectById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var query = new GetSubjectByIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }
    
    [HttpPost]
    [Authorize(Roles = Roles.Admin)]
    [ProducesResponseType(typeof(SubjectResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(IEnumerable<Error>), (int)HttpStatusCode.UnprocessableEntity)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.Conflict)]
    public async Task<IActionResult> CreateSubject([FromBody] CreateSubjectDto dto, CancellationToken cancellationToken)
    {
        var command = new CreateSubjectCommand(dto);
        var result = await _mediator.Send(command, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }

    [HttpPut]
    [Authorize(Roles = Roles.Admin)]
    [ProducesResponseType(typeof(SubjectResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(IEnumerable<Error>), (int)HttpStatusCode.UnprocessableEntity)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.Conflict)]
    public async Task<IActionResult> UpdateSubject([FromBody] UpdateSubjectDto dto, CancellationToken cancellationToken)
    {
        var command = new UpdateSubjectCommand(dto);
        var result = await _mediator.Send(command, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }

    [HttpPut(nameof(AddSubjectToBook))]
    [Authorize(Roles = Roles.Admin)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> AddSubjectToBook([FromBody] AddSubjectToBookDto dto, CancellationToken cancellationToken)
    {
        var command = new AddSubjectToBookCommand(dto);
        var result = await _mediator.Send(command, cancellationToken);
        
        return ApiResponse.GetObjectResult(result, _logger);
    }
    
    [HttpPut(nameof(RemoveSubjectFromBook))]
    [Authorize(Roles = Roles.Admin)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> RemoveSubjectFromBook([FromBody] RemoveSubjectFromBookDto dto, CancellationToken cancellationToken)
    {
        var command = new RemoveSubjectFromBookCommand(dto);
        var result = await _mediator.Send(command, cancellationToken);
        
        return ApiResponse.GetObjectResult(result, _logger);
    }
    
    [HttpDelete("{id:int}")]
    [Authorize(Roles = Roles.Admin)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        var command = new DeleteSubjectCommand(id);
        var result = await _mediator.Send(command, cancellationToken);

        return ApiResponse.GetObjectResult(result, _logger);
    }
}