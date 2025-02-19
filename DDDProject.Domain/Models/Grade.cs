namespace DDDProject.Domain.Models;

public class Grade
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid CourseId { get; set; }
    public double Value { get; set; }

    public Student Student { get; set; }
    public Course Course { get; set; }
    
}