using SchoolManagmentSystem.Application.Common.Exceptions;
using SchoolManagmentSystem.Application.Validators;
using SchoolManagmentSystem.Contract.Dto;
using SchoolManagmentSystem.Contract.IRepositories;
using SchoolManagmentSystem.Contract.IServices;
using SchoolManagmentSystem.Domain.Entities;
using SchoolManagmentSystem.Domain.QueryModels;
using SchoolManagmentSystem.Domain.Specifications;

namespace SchoolManagmentSystem.Application.Services;

public class CourseService : ICourseService
{
    private readonly IRepository<Course> courseRepository;
    private readonly CourseValidator courseValidation;

    public CourseService(IRepository<Course> courseRepository,
                         CourseValidator courseValidation
         )
    {
        this.courseRepository = courseRepository;
        this.courseValidation = courseValidation;
    }
    public async Task<Response<bool>> DeleteCourse(int id)
    {
        CourseSpecification courseSpecification = new CourseSpecification(new CourseQueryModel() { CourseId = id });
        var course = await courseRepository.FirstOrDefaultAsync(courseSpecification);

        if (course == null)
            throw new NotFoundException("Course", id);

        course.IsDeleted = true;
        await courseRepository.SaveChangesAsync();
        return Response<bool>.Success(true);

    }

    public async Task<PagedListDto<CourseDto>> GetCourses(CourseQueryModel courseQuery)
    {
        CourseSpecification courseSpecification = new CourseSpecification(courseQuery);
        var totalRecords = await courseRepository.CountAsync(courseSpecification);
        List<Course> records = await courseRepository.ListAsync(courseSpecification);
        return new PagedListDto<CourseDto>
        {
            List = records?.Select(s => new CourseDto()
            {
                Id = s.Id,
                Name = s.Name
            }).ToList() ?? new List<CourseDto>(),
            TotalCount = totalRecords,
            NumberOfPages = (int)Math.Round((decimal)totalRecords / (courseQuery.PageSize ?? 20))

        };
    }

    public async Task<Response<CourseDto>> SaveCourse(CourseDto courseDto)
    {
        var validationResult = courseValidation.Validate(courseDto);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        Course? course = null;
        if (courseDto.Id == null)
            course = new Course();
        else
        {
            CourseSpecification courseSpecification = new CourseSpecification(new CourseQueryModel() { CourseId = courseDto.Id });
            course = await courseRepository.FirstOrDefaultAsync(courseSpecification);

            if (course == null)
                throw new NotFoundException("Course", courseDto.Id);
        }

        course.Name = courseDto.Name;
        await courseRepository.SaveChangesAsync();

        courseDto.Id = course.Id;
        return Response<CourseDto>.Success(courseDto);
    }
}
