using DDDProject.Application.ViewModels;
using DDDProject.Domain.Models;
using AutoMapper; 
using DDDProject.Domain.Models;
using System.Linq;

namespace DDDProject.Application.Mappings
{
    public class ViewModelMapper : Profile
    {
        public ViewModelMapper()
        {
            CreateMap<Course, CourseViewModel>()
                .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Teacher != null ? $"{src.Teacher.FirstName} {src.Teacher.LastName}" : "Unassigned"))
                .ForMember(dest => dest.CurrentEnrollmentCount, opt => opt.MapFrom(src => src.Enrollments.Count));

            CreateMap<Enrollment, EnrollmentViewModel>()
                .ForMember(dest => dest.StudentFullName, opt => opt.MapFrom(src => src.Student.Name))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.Name));

            CreateMap<Student, StudentViewModel>()
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.EnrolledCourses, opt => opt.MapFrom(src => src.Enrollments.Select(e => e.Course.Name)));

            CreateMap<Grade, GradeViewModel>()
                .ForMember(dest => dest.GradeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.StudentFullName, opt => opt.MapFrom(src => src.Student.Name))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.Name))
                .ForMember(dest => dest.GradeValue, opt => opt.MapFrom(src => (decimal)src.Value));

            CreateMap<Teacher, TeacherViewModel>()
                .ForMember(dest => dest.TeacherId, opt => opt.MapFrom(src => src.TeacherId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.CourseNames, opt => opt.MapFrom(src => src.Courses.Select(c => c.Name)));

            CreateMap<Course, TeacherCourseAssignmentViewModel>()
                .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Teacher != null ? $"{src.Teacher.FirstName} {src.Teacher.LastName}" : "Unassigned"))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.TimeSlot, opt => opt.MapFrom(src => src.TimeSlots.Any() 
                    ? $"{src.TimeSlots.First().StartTime:HH:mm} - {src.TimeSlots.First().EndTime:HH:mm}" 
                    : "Not Assigned"));

            CreateMap<Student, UserProfileViewModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ProfilePictureUrl, opt => opt.MapFrom(src => src.ProfilePicturePath ?? "default_profile.jpg"));
        }
    }
}
