using DDDProject.Domain.Models;

namespace DDDProject.Application.Repositories;

public interface IGradeRepository
{
    Task<Grade> GetByIdAsync(Guid gradeId);
    Task<List<Grade>> GetGradesByStudentAsync(Guid studentId);
    Task<List<Grade>> GetGradesByCourseAsync(Guid courseId);
    Task AddAsync(Grade grade);
    Task UpdateAsync(Grade grade);
    Task DeleteAsync(Guid gradeId);
}

