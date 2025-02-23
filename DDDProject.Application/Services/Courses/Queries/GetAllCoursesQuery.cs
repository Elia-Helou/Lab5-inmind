using DDDProject.Application.ViewModels;
using MediatR;

namespace DDDProject.Application.Services.Courses.Queries;

public record GetAllCoursesQuery() : IRequest<List<CourseViewModel>>;