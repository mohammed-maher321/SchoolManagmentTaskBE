namespace SchoolManagmentSystem.Contract.Dto;

public class PagedListDto<T>
{
    public int TotalCount { get; set; }
    public int NumberOfPages { get; set; }
    public List<T> List { get; set; }
}
