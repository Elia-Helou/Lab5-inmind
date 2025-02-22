namespace DDDProject.Domain.Models;

public class TimeSlot
{
    public Guid Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public Guid? AssignedCourseId { get; set; }
    public Course? AssignedCourse { get; set; }
}
