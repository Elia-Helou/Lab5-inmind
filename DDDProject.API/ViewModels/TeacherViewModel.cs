namespace DDDProject.API.ViewModels;

public class TeacherViewModel
{
    public Guid TeacherId { get; set; }
    public string Name;
    public List<string> CourseNames { get; set; } 
}
