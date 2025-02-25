using Asp.Versioning;
using DDDProject.API.Configurations;
using DDDProject.Application.Repositories;
using DDDProject.Application.Services.Caching;
using DDDProject.Application.Services.Courses.Handlers;
using DDDProject.Application.Services.Teacher.Handlers;
using DDDProject.Domain.Models;
using DDDProject.Infrastructure.Repositories;
using DDDProject.Persistence.DbContext;
using DDDProject.Persistence.Repositories;
using Hangfire;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using DDDProject.Infrastructure.Jobs;
using DDDProject.Infrastructure.Services;
using Serilog;
using Serilog.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHangfire(config =>
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHangfireServer();

builder.Services.AddScoped<EmailNotificationJob>();
builder.Services.AddScoped<RecalculateStudentGradeAverageJob>();
RecurringJob.AddOrUpdate<EmailNotificationJob>(
    "send-email-notifications",
    job => job.ExecuteAsync(),
    Cron.Daily);

RecurringJob.AddOrUpdate<RecalculateStudentGradeAverageJob>(
    "recalculate-student-averages",
    job => job.ExecuteAsync(),
    Cron.Hourly);

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
builder.Services.AddScoped<IEmailService, EmailService>();
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

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration) 
    .Enrich.FromLogContext()
    .Enrich.WithThreadId()
    .Enrich.WithCorrelationId() 
    .WriteTo.Console() 
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day) 
    .WriteTo.Seq("http://localhost:5341") 
    .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddHttpContextAccessor();

builder.Services.AddMemoryCache();  
builder.Services.AddSingleton<ICacheService, CacheService>();

var app = builder.Build();

app.Use(async (context, next) =>
{
    var correlationId = context.Request.Headers["X-Correlation-ID"].FirstOrDefault() ?? Guid.NewGuid().ToString();
    
    using (LogContext.PushProperty("CorrelationId", correlationId))
    {
        context.Response.Headers["X-Correlation-ID"] = correlationId;
        await next();
    }
});

app.UseSerilogRequestLogging();
app.UseHangfireDashboard();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();
