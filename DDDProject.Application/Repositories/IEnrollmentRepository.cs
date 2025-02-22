using DDDProject.Domain.Models;

namespace DDDProject.Application.Repositories;

public interface IEnrollmentRepository
{
    Task<Enrollment> GetByIdAsync(Guid courseId, Guid studentId);
    Task<List<Enrollment>> GetByCourseIdAsync(Guid courseId);
    Task<List<Enrollment>> GetByStudentIdAsync(Guid studentId);
    Task AddAsync(Enrollment enrollment);
    Task UpdateAsync(Enrollment enrollment);
    Task DeleteAsync(Guid courseId, Guid studentId);
}
