namespace DDDProject.API.ViewModels;

public class CourseViewModel
{
    public Guid CourseId { get; set; }
    public string CourseName { get; set; }
    public string Description { get; set; }
    public int MaxStudents { get; set; }
    public DateTime EnrollmentStart { get; set; }
    public DateTime EnrollmentEnd { get; set; }
    public string TeacherName { get; set; } 
    public int CurrentEnrollmentCount { get; set; } 
}