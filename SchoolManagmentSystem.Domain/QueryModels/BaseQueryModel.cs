namespace SchoolManagmentSystem.Domain.QueryModels;

public class BaseQueryModel
{
    public bool? IsPagingEnabled { get; set; } = false;
    public string? OrderByColumn { get; set; }
    public int? PageSize { get; set; }
    public int? PageIndex { get; set; }
    public bool ExculteDeletedRecord { get; set; } = true;
}
