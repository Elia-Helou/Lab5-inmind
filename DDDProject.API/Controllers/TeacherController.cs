using DDDProject.Application.Services.Teacher.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDProject.API.Controllers;

public class TeacherController : ControllerBase
{
    private readonly IMediator _mediator;

    public TeacherController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> RegisterTeacher([FromBody] RegisterTeacherCommand command)
    {
        var result = await _mediator.Send(command);
        if (result)
        {
            return Ok("Teacher and time slots registered successfully.");
        }

        return BadRequest("Failed to register teacher.");
    }
}