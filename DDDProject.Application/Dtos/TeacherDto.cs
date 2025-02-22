namespace DDDProject.Application.Dtos;

public class TeacherDto
{
    public Guid TeacherId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public List<Guid> CoursesTaught { get; set; } 
}