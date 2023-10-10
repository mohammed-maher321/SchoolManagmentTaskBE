using SchoolManagmentSystem.Contract.Dto;
using SchoolManagmentSystem.Domain.QueryModels;

namespace SchoolManagmentSystem.Contract.IServices;

public interface ICourseService
{
    public Task<PagedListDto<CourseDto>> GetCourses(CourseQueryModel courseQuery);
    Task<Response<CourseDto>> SaveCourse(CourseDto courseDto);
    Task<Response<bool>> DeleteCourse(int id);
}
