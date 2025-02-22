using DDDProject.Application.Repositories;
using DDDProject.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDProject.Persistence.DbContext;

namespace DDDProject.Persistence.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _context;

        public CourseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Course> GetByIdAsync(Guid courseId)
        {
            return await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == courseId);
        }

        public async Task<List<Course>> GetAllAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task AddAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid courseId)
        {
            var course = await GetByIdAsync(courseId);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Course>> GetAvailableCoursesAsync(DateTime currentDate)
        {
            return await _context.Courses
                .Where(c => c.EnrollmentStart <= currentDate && c.EnrollmentEnd >= currentDate)
                .ToListAsync();
        }

        public async Task<List<Course>> GetCoursesByTeacherAsync(Guid teacherId)
        {
            return await _context.Courses
                .Where(c => c.TeacherId == teacherId)
                .ToListAsync();
        }

        public async Task<List<Course>> GetCoursesWithStudentsAsync()
        {
            return await _context.Courses
                .Include(c => c.Enrollments)
                    .ThenInclude(e => e.Student)
                .ToListAsync();
        }

        public async Task<List<Student>> GetStudentsInCourseAsync(Guid courseId)
        {
            return await _context.Enrollments
                .Where(e => e.CourseId == courseId)
                .Select(e => e.Student)
                .ToListAsync();
        }
    }
}
