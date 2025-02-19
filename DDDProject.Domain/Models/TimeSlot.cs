namespace DDDProject.Domain.Models;

public class TimeSlot
{
    public Guid Id { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }

    public Guid? AssignedCourseId { get; private set; }
    public Course? AssignedCourse { get; private set; }

}
