namespace SchoolManagmentSystem.Contract.Dto;

public class StudentDto
{
    public int? Id { get; set; }
    public required string Name { get; set; }
    public string? Courses { get; set; }

    public List<int> CourseIds { get; set; }
}
