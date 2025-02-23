using DDDProject.Application.Services.Grade.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GradesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task<IActionResult> AddGrade([FromBody] AddGradeCommand command)
        {
            if (command == null || command.StudentId == Guid.Empty || command.CourseId == Guid.Empty)
            {
                return BadRequest("Invalid data.");
            }

            await _mediator.Send(command);

            return Ok();
        }
    }
}