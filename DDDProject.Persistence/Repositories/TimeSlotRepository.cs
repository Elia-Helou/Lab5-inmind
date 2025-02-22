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
    public class TimeSlotRepository : ITimeSlotRepository
    {
        private readonly AppDbContext _context;

        public TimeSlotRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TimeSlot> GetByIdAsync(Guid timeSlotId)
        {
            return await _context.TimeSlots
                .FirstOrDefaultAsync(t => t.Id == timeSlotId);
        }

        public async Task<List<TimeSlot>> GetAllAsync()
        {
            return await _context.TimeSlots.ToListAsync();
        }

        public async Task AddAsync(TimeSlot timeSlot)
        {
            await _context.TimeSlots.AddAsync(timeSlot);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TimeSlot timeSlot)
        {
            _context.TimeSlots.Update(timeSlot);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid timeSlotId)
        {
            var timeSlot = await GetByIdAsync(timeSlotId);
            if (timeSlot != null)
            {
                _context.TimeSlots.Remove(timeSlot);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AssignTimeSlotToCourseAsync(Guid timeSlotId, Guid courseId)
        {
            var timeSlot = await GetByIdAsync(timeSlotId);
            var course = await _context.Courses.FindAsync(courseId);

            if (timeSlot != null && course != null)
            {
                course.TimeSlots.Add(timeSlot);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<TimeSlot>> GetAvailableTimeSlotsAsync(Guid courseId)
        {
            return await _context.TimeSlots
                .Where(t => !t.AssignedCourseId.HasValue || t.AssignedCourseId != courseId)
                .ToListAsync();
        }
    }
}
