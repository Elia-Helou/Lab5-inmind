using DDDProject.Domain.Models;
using DDDProject.Application.Repositories;
using DDDProject.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DDDProject.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<StudentRepository> _logger;

        public StudentRepository(AppDbContext context, ILogger<StudentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Student> GetByIdAsync(Guid studentId)
        {
            _logger.LogInformation("Fetching student with ID {StudentId}", studentId);

            var student = await _context.Students.FindAsync(studentId);
            if (student == null)
            {
                _logger.LogWarning("Student with ID {StudentId} not found", studentId);
            }

            return student;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all students from the database.");
            return await _context.Students.ToListAsync();
        }

        public async Task AddAsync(Student student)
        {
            _logger.LogInformation("Adding a new student: {StudentName}", student.Name);

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Student {StudentName} added successfully", student.Name);
        }

        public async Task UpdateAsync(Student student)
        {
            _logger.LogInformation("Updating student with ID {StudentId}", student.Id);

            _context.Students.Update(student);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Student {StudentId} updated successfully", student.Id);
        }

        public async Task DeleteAsync(Guid studentId)
        {
            var student = await _context.Students.FindAsync(studentId);
            if (student == null)
            {
                _logger.LogWarning("Attempted to delete non-existent student {StudentId}", studentId);
                return;
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Deleted student {StudentId}", studentId);
        }

        public async Task RecalculateStudentAveragesAsync()
        {
            _logger.LogInformation("Starting recalculation of student grade averages.");

            var students = await _context.Students
                .Include(s => s.Grades)
                .ToListAsync();

            foreach (var student in students)
            {
                if (student.Grades.Any())
                {
                    student.AverageGrade = student.Grades.Average(g => g.Value);
                    student.UpdateCanApplyToFrance();

                    _logger.LogDebug("Updated average grade for student {StudentId}: {AverageGrade}", student.Id, student.AverageGrade);
                }
            }

            await _context.SaveChangesAsync();

            _logger.LogInformation("Successfully recalculated student grade averages.");
        }
    }
}
