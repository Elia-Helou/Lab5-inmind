namespace DDDProject.Domain.Models
{
    public class Student
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        
        public List<Course> Courses { get; private set; } = new();
        
        public List<Grade> Grades { get; private set; } = new();
        
        public bool CanApplyToFrance { get; private set; }
        
        public string? ProfilePicturePath { get; private set; }
        
    }
}
