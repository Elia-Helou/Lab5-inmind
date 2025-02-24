using DDDProject.Application.ViewModels;
using MediatR;

namespace DDDProject.Application.Services.Student.Queries;

public record GetStudentByIdQuery(Guid Id) : IRequest<StudentViewModel>;