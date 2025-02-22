using DDDProject.Application.Repositories;
using DDDProject.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DDDProject.Application.Services.Teacher.Commands;
using DDDProject.Application.ViewModels;

namespace DDDProject.Application.Services.Teacher.Handlers
{
    public class RegisterTeacherCommandHandler : IRequestHandler<RegisterTeacherCommand, bool>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly ITimeSlotRepository _timeSlotRepository;

        public RegisterTeacherCommandHandler(ITeacherRepository teacherRepository, ITimeSlotRepository timeSlotRepository)
        {
            _teacherRepository = teacherRepository;
            _timeSlotRepository = timeSlotRepository;
        }

        public async Task<bool> Handle(RegisterTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = new Domain.Models.Teacher()
            {
                TeacherId = request.TeacherId
            };

            await _teacherRepository.AddAsync(teacher);

            foreach (var slot in request.Slots)
            {
                var timeSlot = new TimeSlot
                {
                    AssignedCourseId = request.CourseId,
                    StartTime = slot.StartTime,
                    EndTime = slot.EndTime
                };

                await _timeSlotRepository.AddAsync(timeSlot);
            }

            return true;
        }
    }
}