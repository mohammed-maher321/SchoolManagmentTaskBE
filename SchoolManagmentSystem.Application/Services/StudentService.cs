using SchoolManagmentSystem.Application.Common.Exceptions;
using SchoolManagmentSystem.Application.Validators;
using SchoolManagmentSystem.Contract.Dto;
using SchoolManagmentSystem.Contract.IRepositories;
using SchoolManagmentSystem.Contract.IServices;
using SchoolManagmentSystem.Domain.Entities;
using SchoolManagmentSystem.Domain.QueryModels;
using SchoolManagmentSystem.Domain.Specifications;
using System.Numerics;

namespace SchoolManagmentSystem.Application.Services;

public class StudentService : IStudentService
{
    private readonly IRepository<Student> studentRepository;
    private readonly StudentValidator studentValidation;

    public StudentService(IRepository<Student> studentRepository,
                         StudentValidator studentValidation
         )
    {
        this.studentRepository = studentRepository;
        this.studentValidation = studentValidation;
    }

    public async Task<Response<bool>> DeleteStudent(int id)
    {
        StudentSpecification studentSpecification = new StudentSpecification(new StudentQueryModel() { StudentId = id });
        var student = await studentRepository.FirstOrDefaultAsync(studentSpecification);

        if (student == null)
            throw new NotFoundException("Student", id);

        student.IsDeleted = true;
        await studentRepository.SaveChangesAsync();
        return Response<bool>.Success(true);
    }


    public async Task<Response<StudentDto>> GetStudent(int id)
    {
        StudentSpecification studentSpecification = new StudentSpecification(new StudentQueryModel() { StudentId = id });
        var student = await studentRepository.FirstOrDefaultAsync(studentSpecification);

        if (student == null)
            throw new NotFoundException("Student", id);

        return Response<StudentDto>.Success(new StudentDto()
        {
            Id = student.Id,
            Name = student.Name,
            //CourseIds = student.StudentCourses.Select(c => c.CourseId).ToList(),
            Courses = string.Join(",", student.StudentCourses.Select(c => c.Course?.Name ?? "").ToList())
        });
    }

    public async Task<PagedListDto<StudentDto>> GetStudents(StudentQueryModel studentQuery)
    {
        studentQuery.IsPagingEnabled = true;
        StudentSpecification studentSpecification = new StudentSpecification(studentQuery);
        var totalRecords = await studentRepository.CountAsync(studentSpecification);
        List<Student> records = await studentRepository.ListAsync(studentSpecification);
        return new PagedListDto<StudentDto>
        {
            List = records?.Select(s => new StudentDto()
            {
                Id = s.Id,
                Name = s.Name,
                Courses = string.Join("," , s.StudentCourses.Select(c => c.Course?.Name ?? "").ToList())
            }).ToList() ?? new List<StudentDto>(),
            TotalCount = totalRecords,
            NumberOfPages = (int)Math.Round((decimal)totalRecords / (studentQuery.PageSize ?? 20))

        };
    }

    public async Task<Response<StudentDto>> SaveStudent(StudentDto studentDto)
    {
        var validationResult = studentValidation.Validate(studentDto);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        Student? student = null;
        if (studentDto.Id == null)
        {
            student = new Student()
            {
                Name = studentDto.Name,
                StudentCourses = studentDto.CourseIds.Select(s => new StudentCourse() { CourseId = s }).ToList()
            };
            await studentRepository.AddAsync(student);
        }
        else
        {
            StudentSpecification studentSpecification = new StudentSpecification(new StudentQueryModel() { StudentId = studentDto.Id });
            student = await studentRepository.FirstOrDefaultAsync(studentSpecification);

            if (student == null)
                throw new NotFoundException("student", studentDto.Id);
            student.Name = studentDto.Name;
            student.StudentCourses.Clear();
            student.StudentCourses = studentDto.CourseIds.Select(s => new StudentCourse() { CourseId = s }).ToList();
        }

        
        await studentRepository.SaveChangesAsync();

        studentDto.Id = student.Id;
        return Response<StudentDto>.Success(studentDto);
    }
}
