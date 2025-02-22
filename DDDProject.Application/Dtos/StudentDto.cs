namespace DDDProject.Application.Dtos;

public class StudentDTO
{
    public Guid StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
}