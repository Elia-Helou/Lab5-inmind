using DDDProject.Application.Repositories;
using DDDProject.Application.Services.Caching;
using DDDProject.Application.Services.Student.Queries;
using DDDProject.Application.ViewModels;
using MediatR;

public class GetStudentByIdHandler : IRequestHandler<GetStudentByIdQuery, StudentViewModel>
{
    private readonly ICacheService _cacheService;
    private readonly IStudentRepository _studentRepository;

    public GetStudentByIdHandler(ICacheService cacheService, IStudentRepository studentRepository)
    {
        _cacheService = cacheService;
        _studentRepository = studentRepository;
    }

    public async Task<StudentViewModel> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        string cacheKey = $"student_{request.Id}";

        var studentViewModel = _cacheService.Get<StudentViewModel>(cacheKey);
        if (studentViewModel != null)
        {
            return studentViewModel;
        }

        var student = await _studentRepository.GetByIdAsync(request.Id);
        if (student == null) return null;

        studentViewModel = new StudentViewModel
        {
            StudentId = student.Id,
            Name = student.Name,
        };

        _cacheService.Set(cacheKey, studentViewModel, TimeSpan.FromMinutes(10));

        return studentViewModel;
    }
}