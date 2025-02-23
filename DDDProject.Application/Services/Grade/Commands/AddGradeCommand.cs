using MediatR;

namespace DDDProject.Application.Services.Grade.Commands;

public class AddGradeCommand : IRequest
{
    public Guid StudentId { get; set; }
    public Guid CourseId { get; set; }
    public double GradeValue { get; set; }
}