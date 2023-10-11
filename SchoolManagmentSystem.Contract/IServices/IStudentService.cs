using SchoolManagmentSystem.Contract.Dto;
using SchoolManagmentSystem.Domain.QueryModels;

namespace SchoolManagmentSystem.Contract.IServices;

public interface IStudentService
{
    public Task<PagedListDto<StudentDto>> GetStudents(StudentQueryModel studentQuery);
    Task<Response<StudentDto>> SaveStudent(StudentDto studentDto);
    Task<Response<StudentDto>> GetStudent(int id);
    Task<Response<bool>> DeleteStudent(int id);
}
