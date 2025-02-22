using DDDProject.Domain.Models;
namespace DDDProject.Application.Repositories;

public interface ITimeSlotRepository
{
    Task<TimeSlot> GetByIdAsync(Guid timeSlotId);
    Task<List<TimeSlot>> GetAllAsync();
    Task AddAsync(TimeSlot timeSlot);
    Task UpdateAsync(TimeSlot timeSlot);
    Task DeleteAsync(Guid timeSlotId);
    Task AssignTimeSlotToCourseAsync(Guid timeSlotId, Guid courseId);
    Task<List<TimeSlot>> GetAvailableTimeSlotsAsync(Guid courseId);
}

