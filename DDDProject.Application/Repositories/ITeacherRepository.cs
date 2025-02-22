using DDDProject.Domain.Models;

namespace DDDProject.Application.Repositories;

public interface ITeacherRepository
{
    Task<Teacher> GetByIdAsync(Guid teacherId);
    Task<List<Teacher>> GetAllAsync();
    Task AddAsync(Teacher teacher);
    Task UpdateAsync(Teacher teacher); 
    Task DeleteAsync(Guid teacherId);
}

