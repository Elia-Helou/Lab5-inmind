using Microsoft.EntityFrameworkCore;

namespace DDDProject.Persistence.DbContext;

public class AppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<TimeSlot> TimeSlots { get; set; }
}