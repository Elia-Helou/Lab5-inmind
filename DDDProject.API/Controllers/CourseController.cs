using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using MediatR;
using DDDProject.Application.Services.Courses.Queries;
using DDDProject.Application.ViewModels;

namespace DDDProject.API.Controllers
{
    [Route("api/courses")]
    [ApiController]
    [ApiVersion(1.0)]
    public class CoursesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CoursesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [EnableQuery] 
        public async Task<ActionResult<IEnumerable<CourseViewModel>>> GetCourses()
        {
            var courses = await _mediator.Send(new GetAllCoursesQuery());
            return Ok(courses);
        }
    }
}
