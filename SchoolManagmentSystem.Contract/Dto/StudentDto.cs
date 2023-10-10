namespace SchoolManagmentSystem.Contract.Dto;

public class StudentDto
{
    public int? Id { get; set; }
    public required string Name { get; set; }

    public List<CourseDto> Courses { get; set; }
}
