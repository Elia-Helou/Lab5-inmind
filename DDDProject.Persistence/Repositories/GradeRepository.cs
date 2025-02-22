using DDDProject.Domain.Models;
using DDDProject.Application.Repositories;
using DDDProject.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DDDProject.Infrastructure.Repositories
{
    public class GradeRepository : IGradeRepository
    {
        private readonly AppDbContext _context;

        public GradeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Grade> GetByIdAsync(Guid gradeId)
        {
            return await _context.Grades.FindAsync(gradeId);
        }

        public async Task<List<Grade>> GetGradesByStudentAsync(Guid studentId)
        {
            return await _context.Grades.Where(g => g.StudentId == studentId).ToListAsync();
        }

        public async Task<List<Grade>> GetGradesByCourseAsync(Guid courseId)
        {
            return await _context.Grades.Where(g => g.CourseId == courseId).ToListAsync();
        }

        public async Task AddAsync(Grade grade)
        {
            await _context.Grades.AddAsync(grade);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Grade grade)
        {
            _context.Grades.Update(grade);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid gradeId)
        {
            var grade = await _context.Grades.FindAsync(gradeId);
            if (grade != null)
            {
                _context.Grades.Remove(grade);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddGradeAsync(Guid studentId, Guid courseId, decimal gradeValue)
        {
            var grade = new Grade
            {
                Id = Guid.NewGuid(),
                StudentId = studentId,
                CourseId = courseId,
                Value = (double)gradeValue
            };

            await _context.Grades.AddAsync(grade);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Grade>> GetGradesForStudentAsync(Guid studentId)
        {
            return await _context.Grades
                .Where(g => g.StudentId == studentId)
                .ToListAsync();
        }
        
    }

}