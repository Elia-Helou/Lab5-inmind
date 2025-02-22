namespace DDDProject.API.ViewModels;

public class StudentViewModel
{
    public Guid StudentId { get; set; }
    public string Name;
    public double GPA { get; set; } // Could be calculated based on grades
    public List<string> EnrolledCourses { get; set; }
}