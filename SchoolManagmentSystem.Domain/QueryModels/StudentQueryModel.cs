namespace SchoolManagmentSystem.Domain.QueryModels;

public class StudentQueryModel : BaseQueryModel
{
    public int? StudentId { get; set; }
    public string? StudentName { get; set; }
    public string? KeyWord { get; set; }
    public int? CourseId { get; set; }
}
