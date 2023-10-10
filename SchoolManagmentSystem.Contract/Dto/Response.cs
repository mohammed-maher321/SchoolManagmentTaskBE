namespace SchoolManagmentSystem.Contract.Dto;

public class Response<T>
{
    internal Response(bool succeeded, IEnumerable<string> errors, T model)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
        Model = model;
    }

    public bool Succeeded { get; set; }

    public string[] Errors { get; set; }

    public T Model { get; set; }

    public static Response<T> Success(T model)
    {
        return new Response<T>(true, new string[] { }, model);
    }

    public static Response<T> Failure(IEnumerable<string> errors, T model)
    {
        return new Response<T>(false, errors, model);
    }
}