namespace DDDProject.Application.ViewModels;

public class TeacherViewModel
{
    public Guid TeacherId { get; set; }
    public string Name;
    public List<string> CourseNames { get; set; } 
}
