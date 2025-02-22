using MediatR;

namespace DDDProject.Application.Services.Courses.Commands;

public record CreateCourseCommand(string Name, int MaxStudents, DateTime EnrollmentStart, DateTime EnrollmentEnd) : IRequest<Guid>;