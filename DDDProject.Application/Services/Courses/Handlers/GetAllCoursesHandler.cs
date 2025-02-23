using System.Text.Json;
using DDDProject.Application.Repositories;
using DDDProject.Application.Services.Courses.Queries;
using DDDProject.Application.ViewModels;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace DDDProject.Application.Services.Courses.Handlers;

public class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, List<CourseViewModel>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IDistributedCache _cache;
    private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(30); 

    public GetAllCoursesQueryHandler(ICourseRepository courseRepository, IDistributedCache cache)
    {
        _courseRepository = courseRepository;
        _cache = cache;
    }

    public async Task<List<CourseViewModel>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
    {
        const string cacheKey = "all_courses";

        var cachedData = await _cache.GetStringAsync(cacheKey, cancellationToken);
        if (!string.IsNullOrEmpty(cachedData))
        {
            return JsonSerializer.Deserialize<List<CourseViewModel>>(cachedData)!;
        }

        var courses = await _courseRepository.GetAllAsync();

        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = _cacheExpiration
        };

        var serializedData = JsonSerializer.Serialize(courses);
        await _cache.SetStringAsync(cacheKey, serializedData, options, cancellationToken);

        return courses;
    }
}