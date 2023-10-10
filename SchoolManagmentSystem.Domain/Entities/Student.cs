namespace SchoolManagmentSystem.Domain.Entities;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsDeleted { get; set; }

    public virtual ICollection<StudentCourse> StudentCourses { get; set; }

}
