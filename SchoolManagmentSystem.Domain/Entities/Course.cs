namespace SchoolManagmentSystem.Domain.Entities;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsDeleted { get; set; }

    public virtual ICollection<StudentCourse> StudentCourses { get; set; }
}
