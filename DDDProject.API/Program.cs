using Asp.Versioning;
using DDDProject.API.Configurations;
using DDDProject.Application.Repositories;
using DDDProject.Application.Services.Courses.Handlers;
using DDDProject.Application.Services.Teacher.Handlers;
using DDDProject.Domain.Models;
using DDDProject.Infrastructure.Repositories;
using DDDProject.Persistence.DbContext;
using DDDProject.Persistence.Repositories;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiVersioning(options =>
    {
        var apiVersioningConfig = builder.Configuration.GetSection("ApiVersioning").Get<ApiVersioningConfig>();

        options.DefaultApiVersion = new ApiVersion(apiVersioningConfig.DefaultMajorVersion, apiVersioningConfig.DefaultMinorVersion);
        options.AssumeDefaultVersionWhenUnspecified = apiVersioningConfig.AssumeDefaultVersion;
        options.ReportApiVersions = apiVersioningConfig.ReportApiVersions;
        options.ApiVersionReader = new UrlSegmentApiVersionReader(); 
    })
    
    .AddMvc();
builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(typeof(CreateCourseHandler).Assembly);
    configuration.RegisterServicesFromAssembly(typeof(RegisterTeacherCommandHandler).Assembly);
    configuration.RegisterServicesFromAssembly(typeof(CreateCourseHandler).Assembly);
});

builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ITimeSlotRepository, TimeSlotRepository>();
builder.Services.AddScoped<IGradeRepository, GradeRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
    options.InstanceName = "UMS_"; 
});

builder.Services.AddControllers()
    .AddOData(options =>
    {
        var modelBuilder = new ODataConventionModelBuilder();
        modelBuilder.EntitySet<Teacher>("Teachers");
        modelBuilder.EntitySet<Student>("Students");
        modelBuilder.EntitySet<Enrollment>("Enrollments");
        modelBuilder.EntitySet<Grade>("Grades");
        modelBuilder.EntitySet<TimeSlot>("TimeSlots");
        modelBuilder.EntitySet<Course>("Courses");
        
        options.EnableQueryFeatures();  
        options.AddRouteComponents("odata", modelBuilder.GetEdmModel());
    });

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();