using DDDProject.Domain.Models;

namespace DDDProject.Application.Repositories;

public interface ITeacherRepository
{
    Task<Teacher> GetByIdAsync(Guid teacherId);
    Task<List<Teacher>> GetAllAsync();
    Task AddAsync(Teacher teacher);
    Task UpdateAsync(Teacher teacher);
    Task DeleteAsync(Guid teacherId);
    Task AssignTeacherToCourseAsync(Guid teacherId, Guid courseId);
    Task<List<Course>> GetCoursesByTeacherAsync(Guid teacherId);
}


