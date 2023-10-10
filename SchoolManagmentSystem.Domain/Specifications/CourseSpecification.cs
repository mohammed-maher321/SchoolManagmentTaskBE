using Ardalis.Specification;
using SchoolManagmentSystem.Domain.Entities;
using SchoolManagmentSystem.Domain.QueryModels;

namespace SchoolManagmentSystem.Domain.Specifications;

public class CourseSpecification : Specification<Course>
{
    public CourseSpecification(CourseQueryModel spec)
    {
        Query
            .Where(c =>
            !string.IsNullOrWhiteSpace(spec.CourseName) ? c.Name.Contains(spec.CourseName) : true &&
            spec.CourseId != null ? c.Id == spec.CourseId : true &&
            c.IsDeleted == spec.ExculteDeletedRecord 
            );

        if (spec.IsPagingEnabled ?? false)
            Query.Skip((spec.PageIndex.Value - 1) * spec.PageSize.Value).Take(spec.PageSize.Value);
    }
}
