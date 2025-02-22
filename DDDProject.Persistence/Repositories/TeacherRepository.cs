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
    public class TeacherRepository : ITeacherRepository
    {
        private readonly AppDbContext _context;

        public TeacherRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Teacher> GetByIdAsync(Guid teacherId)
        {
            return await _context.Teachers
                .FirstOrDefaultAsync(t => t.TeacherId == teacherId);
        }

        public async Task<List<Teacher>> GetAllAsync()
        {
            return await _context.Teachers.ToListAsync();
        }

        public async Task AddAsync(Teacher teacher)
        {
            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Teacher teacher)
        {
            _context.Teachers.Update(teacher);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid teacherId)
        {
            var teacher = await GetByIdAsync(teacherId);
            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Teacher>> GetTeachersForCourseAsync(Guid courseId)
        {
            return await _context.Teachers
                .Where(t => t.Courses.Any(c => c.Id == courseId))
                .ToListAsync();
        }

        public async Task AssignTeacherToCourseAsync(Guid teacherId, Guid courseId)
        {
            var teacher = await GetByIdAsync(teacherId);
            var course = await _context.Courses.FindAsync(courseId);

            if (teacher != null && course != null)
            {
                course.TeacherId = teacherId;
                _context.Courses.Update(course);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Course>> GetCoursesByTeacherAsync(Guid teacherId)
        {
            var teacher = await _context.Teachers
                .Where(t => t.TeacherId == teacherId)
                .Include(t => t.Courses) 
                .FirstOrDefaultAsync();

            if (teacher != null)
            {
                return teacher.Courses;  
            }

            return new List<Course>();

           
        }
    }
}
