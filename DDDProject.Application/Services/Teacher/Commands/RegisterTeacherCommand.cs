using DDDProject.Domain.Models;
using MediatR;

namespace DDDProject.Application.Services.Teacher.Commands;

public record RegisterTeacherCommand(Guid TeacherId, Guid CourseId, List<TimeSlot> Slots) : IRequest<bool>;