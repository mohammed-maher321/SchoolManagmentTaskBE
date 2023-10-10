using FluentValidation;
using SchoolManagmentSystem.Contract.Dto;
using SchoolManagmentSystem.Contract.IRepositories;
using SchoolManagmentSystem.Contract.Resourses;
using SchoolManagmentSystem.Domain.Entities;
using SchoolManagmentSystem.Domain.QueryModels;
using SchoolManagmentSystem.Domain.Specifications;

namespace SchoolManagmentSystem.Application.Validators;


public class CourseValidator : AbstractValidator<CourseDto>
{
    public CourseValidator(IRepository<Course> courseRepository)
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage(ShcoolManagmentSystemResource.CourseNameRequired)
                                .NotNull().WithMessage(ShcoolManagmentSystemResource.CourseNameRequired)
                                .MaximumLength(225).WithMessage(ShcoolManagmentSystemResource.CourseNameLength);

        RuleFor(x => x)
        .Must(a =>
        {
            if (!string.IsNullOrWhiteSpace(a.Name))
            {

                var specification = new CourseSpecification(new CourseQueryModel() { CourseName = a.Name });
                var result = courseRepository.FirstOrDefaultAsync(specification).Result;
                if (result != null && result.Id != a.Id)
                    return false;
            }
            return true;
        })
        .WithMessage(ShcoolManagmentSystemResource.CourseNameUnique);


    }
}