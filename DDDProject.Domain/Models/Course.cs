namespace DDDProject.Domain.Models
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int MaxStudents { get; set; }
        public DateTime EnrollmentStart { get; set; }
        public DateTime EnrollmentEnd { get; set; }
        
        public List<Student> Students { get; set; } = new();

        public Guid? TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
    }
}