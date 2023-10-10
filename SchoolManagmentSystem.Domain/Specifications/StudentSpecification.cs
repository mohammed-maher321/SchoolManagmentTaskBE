using Ardalis.Specification;
using SchoolManagmentSystem.Domain.Entities;
using SchoolManagmentSystem.Domain.QueryModels;

namespace SchoolManagmentSystem.Domain.Specifications;

public class StudentSpecification : Specification<Student>
{
    public StudentSpecification(StudentQueryModel spec)
    {
        Query
            .Include(s => s.StudentCourses)
            .ThenInclude(s => s.Course)
            .Where(c =>
            !string.IsNullOrWhiteSpace(spec.KeyWord) ? c.Name.Contains(spec.KeyWord) || c.StudentCourses.Where(a => a.Course.Name.Contains(spec.KeyWord)).Count() > 0 : true &&
            !string.IsNullOrWhiteSpace(spec.StudentName) ? c.Name.Contains(spec.StudentName) : true &&
            spec.CourseId != null ? c.StudentCourses.Where(s => s.CourseId == spec.CourseId).Count() > 0 : true &&
            spec.StudentId != null ? c.Id == spec.StudentId : true &&
            c.IsDeleted == spec.ExculteDeletedRecord &&
            c.StudentCourses.Where(a => a.Course.IsDeleted == spec.ExculteDeletedRecord).Count() > 0
        );

        if (spec.IsPagingEnabled ?? false)
            Query.Skip((spec.PageIndex.Value - 1) * spec.PageSize.Value).Take(spec.PageSize.Value);
    }
}
