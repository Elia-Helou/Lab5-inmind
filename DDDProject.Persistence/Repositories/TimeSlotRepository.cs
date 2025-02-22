using DDDProject.Application.Repositories;
using DDDProject.Domain.Models;
using DDDProject.Infrastructure;
using DDDProject.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DDDProject.Infrastructure.Repositories
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
    }
}