namespace DDDProject.Domain.Models
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int MaxStudents { get; set; }
        public DateTime EnrollmentStart { get; set; }
        public DateTime EnrollmentEnd { get; set; }

        public List<Enrollment> Enrollments { get; set; } = new();
        public Guid? TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
        public List<TimeSlot> TimeSlots { get; set; } = new();
        public List<Grade> Grades { get; set; } = new();
    }
}