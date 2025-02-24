using DDDProject.Application.Repositories;
using DDDProject.Application.Services.Courses.Commands;
using DDDProject.Domain.Models;
using MediatR;

namespace DDDProject.Application.Services.Courses.Handlers;

public class CreateCourseHandler : IRequestHandler<CreateCourseCommand, Guid>
{
    private readonly ICourseRepository _courseRepository;

    public CreateCourseHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<Guid> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = new Course
        {
            Name = request.Name,
            MaxStudents = request.MaxStudents,
            EnrollmentStart = request.EnrollmentStart,
            EnrollmentEnd = request.EnrollmentEnd
        };

        await _courseRepository.AddAsync(course);
        return course.Id;
    }
    
}