using FluentValidation;
using SchoolManagmentSystem.Contract.Dto;
using SchoolManagmentSystem.Contract.IRepositories;
using SchoolManagmentSystem.Contract.Resourses;
using SchoolManagmentSystem.Domain.Entities;
using SchoolManagmentSystem.Domain.QueryModels;
using SchoolManagmentSystem.Domain.Specifications;

namespace SchoolManagmentSystem.Application.Validators;


public class StudentValidator : AbstractValidator<StudentDto>
{
    public StudentValidator(IRepository<Student> studentRepository)
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage(ShcoolManagmentSystemResource.StudentNameRequired)
                               .NotNull().WithMessage(ShcoolManagmentSystemResource.StudentNameRequired)
                               .MaximumLength(50).WithMessage(ShcoolManagmentSystemResource.StudentNameLength);

        RuleFor(x => x.Courses).NotEmpty().WithMessage(ShcoolManagmentSystemResource.CoursesReqired)
                               .NotNull().WithMessage(ShcoolManagmentSystemResource.CoursesReqired);

        RuleFor(x => x)
        .Must(a =>
        {
            if (!string.IsNullOrWhiteSpace(a.Name))
            {

                var specification = new StudentSpecification(new StudentQueryModel() { StudentName = a.Name });
                var result = studentRepository.FirstOrDefaultAsync(specification).Result;
                if (result != null && result.Id != a.Id)
                    return false;
            }
            return true;
        })
        .WithMessage(ShcoolManagmentSystemResource.StudentNameUnique);


    }
}