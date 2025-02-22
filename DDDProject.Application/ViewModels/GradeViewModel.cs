namespace DDDProject.Application.ViewModels;

public class GradeViewModel
{
    public Guid GradeId { get; set; }
    public string StudentFullName { get; set; }
    public string CourseName { get; set; }
    public decimal GradeValue { get; set; }
    public DateTime DateAssigned { get; set; }
}
