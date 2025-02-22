using DDDProject.Domain.Models;

namespace DDDProject.Application.Repositories;

public interface ICourseRepository
{
    Task<Course> GetByIdAsync(Guid courseId);
    Task<List<Course>> GetAllAsync();
    Task AddAsync(Course course);
    Task UpdateAsync(Course course);
    Task DeleteAsync(Guid courseId);
    Task<List<Course>> GetCoursesByTeacherAsync(Guid teacherId);
    Task<List<Course>> GetCoursesByStudentAsync(Guid studentId);
}
