namespace DDDProject.Application.Dtos;

public class CourseDto
{
    public Guid CourseId { get; set; }
    public string CourseName { get; set; }
    public string Description { get; set; }
    public int MaxStudents { get; set; }
    public DateTime EnrollmentStart { get; set; }
    public DateTime EnrollmentEnd { get; set; }
}