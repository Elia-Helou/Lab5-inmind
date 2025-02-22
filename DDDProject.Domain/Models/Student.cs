namespace DDDProject.Domain.Models
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Enrollment> Enrollments { get; set; } = new();
        public List<Grade> Grades { get; set; } = new();
        public bool CanApplyToFrance { get; set; }
        public string? ProfilePicturePath { get; set; }

        public double AverageGrade => Grades.Any() ? Grades.Average(g => g.Value) : 0;

        public void UpdateCanApplyToFrance()
        {
            CanApplyToFrance = AverageGrade >= 15;
        }
    }
}
