﻿using DDDProject.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DDDProject.Persistence.DbContext;

public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<TimeSlot> TimeSlots { get; set; }
    
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

    //{
      //  optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=umsdb;Username=postgres;Password=postgrespassword");
    //}
    
}