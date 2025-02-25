using DDDProject.Application.Repositories;

namespace DDDProject.Infrastructure.Jobs;

public class RecalculateStudentGradeAverageJob
{
    private readonly IStudentRepository _studentRepository;

    public RecalculateStudentGradeAverageJob(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task ExecuteAsync()
    {
        var students = await _studentRepository.GetAllAsync();

        foreach (var student in students)
        {
            var averageGrade = student.Grades.Any() 
                ? student.Grades.Average(g => g.Value) 
                : 0;

            student.AverageGrade = averageGrade;

            await _studentRepository.UpdateAsync(student);
        }
    }
}
