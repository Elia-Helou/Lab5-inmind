namespace DDDProject.Domain.Models;

public class Teacher
{
    public Guid TeacherId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public List<Course> Courses { get; private set; } = new();
}