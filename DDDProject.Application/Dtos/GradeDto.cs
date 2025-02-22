namespace DDDProject.Application.Dtos;

public class GradeDto
{
    public Guid GradeId { get; set; }
    public Guid StudentId { get; set; }
    public Guid CourseId { get; set; }
    public decimal GradeValue { get; set; }
    public DateTime DateAssigned { get; set; }
}