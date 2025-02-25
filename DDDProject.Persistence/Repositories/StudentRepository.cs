using DDDProject.Domain.Models;
using DDDProject.Application.Repositories;
using DDDProject.Persistence.DbContext;
using DDDProject.Infrastructure.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DDDProject.Persistence.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<StudentRepository> _logger;
        private readonly SharedLocalizer _localizer;

        public StudentRepository(AppDbContext context, ILogger<StudentRepository> logger, SharedLocalizer localizer)
        {
            _context = context;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task<Student> GetByIdAsync(Guid studentId)
        {
            _logger.LogInformation(_localizer.Get("Fetching student with ID {0}"), studentId);

            var student = await _context.Students.FindAsync(studentId);
            if (student == null)
            {
                _logger.LogWarning(_localizer.Get("Student with ID {0} not found"), studentId);
            }

            return student;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            _logger.LogInformation(_localizer.Get("Fetching all students from the database."));
            return await _context.Students.ToListAsync();
        }

        public async Task AddAsync(Student student)
        {
            _logger.LogInformation(_localizer.Get("Adding a new student: {0}"), student.Name);

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

            _logger.LogInformation(_localizer.Get("Student {0} added successfully"), student.Name);
        }

        public async Task UpdateAsync(Student student)
        {
            _logger.LogInformation(_localizer.Get("Updating student with ID {0}"), student.Id);

            _context.Students.Update(student);
            await _context.SaveChangesAsync();

            _logger.LogInformation(_localizer.Get("Student {0} updated successfully"), student.Id);
        }

        public async Task DeleteAsync(Guid studentId)
        {
            var student = await _context.Students.FindAsync(studentId);
            if (student == null)
            {
                _logger.LogWarning(_localizer.Get("Attempted to delete non-existent student {0}"), studentId);
                return;
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            _logger.LogInformation(_localizer.Get("Deleted student {0}"), studentId);
        }

        public async Task RecalculateStudentAveragesAsync()
        {
            _logger.LogInformation(_localizer.Get("Starting recalculation of student grade averages."));

            var students = await _context.Students
                .Include(s => s.Grades)
                .ToListAsync();

            foreach (var student in students)
            {
                if (student.Grades.Any())
                {
                    student.AverageGrade = student.Grades.Average(g => g.Value);
                    student.UpdateCanApplyToFrance();

                    _logger.LogDebug(_localizer.Get("Updated average grade for student {0}: {1}"), student.Id, student.AverageGrade);
                }
            }

            await _context.SaveChangesAsync();

            _logger.LogInformation(_localizer.Get("Successfully recalculated student grade averages."));
        }
    }
}
