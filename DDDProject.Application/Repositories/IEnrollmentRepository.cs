using DDDProject.Domain.Models;

namespace DDDProject.Application.Repositories;

public interface IEnrollmentRepository
{
    Task<Enrollment> GetByIdAsync(Guid enrollmentId);
    Task<List<Enrollment>> GetAllAsync();
    Task AddAsync(Enrollment enrollment);
    Task UpdateAsync(Enrollment enrollment);
    Task DeleteAsync(Guid enrollmentId);
    Task EnrollStudentAsync(Guid studentId, Guid courseId, DateTime enrollmentDate);
    Task<List<Enrollment>> GetEnrollmentsByStudentAsync(Guid studentId);
    Task RemoveEnrollmentAsync(Guid studentId, Guid courseId);
}

