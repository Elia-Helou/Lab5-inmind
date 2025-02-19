namespace DDDProject.Domain.Models;

public class Enrollment
{
    public Guid Id { get; private set; }
    public Guid StudentId { get; private set; }
    public Guid CourseId { get; private set; }
    public DateTime EnrollmentDate { get; private set; }

    public Student Student { get; private set; }
    public Course Course { get; private set; }
}
