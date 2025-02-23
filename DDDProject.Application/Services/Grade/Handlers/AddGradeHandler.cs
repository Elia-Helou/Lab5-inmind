using DDDProject.Application.Repositories;
using DDDProject.Application.Services.Grade.Commands;
using MediatR;

namespace DDDProject.Application.Services.Grade.Handlers;

public class AddGradeHandler : IRequestHandler<AddGradeCommand>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IGradeRepository _gradeRepository;
    private readonly ICourseRepository _courseRepository;

    public AddGradeHandler(IStudentRepository studentRepository, IGradeRepository gradeRepository, ICourseRepository courseRepository)
    {
        _studentRepository = studentRepository;
        _gradeRepository = gradeRepository;
        _courseRepository = courseRepository;
    }

    public async Task Handle(AddGradeCommand request, CancellationToken cancellationToken)
    {
        // Step 1: Add grade
        var grade = new Domain.Models.Grade
        {
            Id = Guid.NewGuid(),
            StudentId = request.StudentId,
            CourseId = request.CourseId,
            Value = request.GradeValue
        };

        await _gradeRepository.AddAsync(grade);

        var grades = await _gradeRepository.GetGradesForStudentAsync(studentId: request.StudentId);
        var averageGrade = grades.Average(g => g.Value);

        var student = await _studentRepository.GetByIdAsync(request.StudentId);
        student.AverageGrade = averageGrade;

        if (student.AverageGrade > 15)
        {
            student.CanApplyToFrance = true;
        }

        await _studentRepository.UpdateAsync(student);
    }
}
