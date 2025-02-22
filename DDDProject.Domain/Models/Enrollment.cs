namespace DDDProject.Domain.Models;

public class Enrollment
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid CourseId { get; set; }
    public DateTime EnrollmentDate { get; set; }

    public Student Student { get; set; }
    public Course Course { get; set; }
}
