using DDDProject.Domain.Models;

namespace DDDProject.Application.Repositories;

public interface IGradeRepository
{
    Task<Grade> GetByIdAsync(Guid gradeId);
    Task AddAsync(Grade grade);
    Task UpdateAsync(Grade grade);
    Task DeleteAsync(Guid gradeId);
    Task AddGradeAsync(Guid studentId, Guid courseId, decimal grade);
    Task<List<Grade>> GetGradesForStudentAsync(Guid studentId);
}


