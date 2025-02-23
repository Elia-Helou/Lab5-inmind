using DDDProject.Application.ViewModels;
using DDDProject.Domain.Models;

namespace DDDProject.Application.Repositories;

public interface ICourseRepository
{
    Task<Course> GetByIdAsync(Guid courseId);
    Task<List<CourseViewModel>> GetAllAsync();
    Task AddAsync(Course course);
    Task UpdateAsync(Course course);
    Task DeleteAsync(Guid courseId);
    Task<List<Course>> GetAvailableCoursesAsync(DateTime currentDate);
    Task<List<Course>> GetCoursesByTeacherAsync(Guid teacherId);
    Task<List<Course>> GetCoursesWithStudentsAsync();
}

