using DDDProject.Domain.Models;
using DDDProject.Application.Repositories;
using DDDProject.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DDDProject.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Student> GetByIdAsync(Guid studentId)
        {
            return await _context.Students.FindAsync(studentId);
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task AddAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid studentId)
        {
            var student = await _context.Students.FindAsync(studentId);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }
        
        public async Task RecalculateStudentAveragesAsync()
        {
            var students = await _context.Students
                .Include(s => s.Grades) 
                .ToListAsync();

            foreach (var student in students)
            {
                if (student.Grades.Any())
                {
                    student.AverageGrade = student.Grades.Average(g => g.Value);
                    student.UpdateCanApplyToFrance(); 
                }
            }

            await _context.SaveChangesAsync();
            Console.WriteLine("Student grade averages recalculated.");
        }
    }
}