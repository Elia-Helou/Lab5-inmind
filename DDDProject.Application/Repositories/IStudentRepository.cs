using DDDProject.Domain.Models;

namespace DDDProject.Application.Repositories;

public interface IStudentRepository
{
    Task<Student> GetByIdAsync(Guid studentId);
    Task<List<Student>> GetAllAsync();
    Task AddAsync(Student student);
    Task UpdateAsync(Student student);
    Task DeleteAsync(Guid studentId);
    Task RecalculateStudentAveragesAsync();
}

