using FluentValidation.Results;

namespace SchoolManagmentSystem.Application.Common.Exceptions;
public class ValidationException : Exception 
{
    public ValidationException() 
        : base("One or more validation failures have occurred.") 
    {
        Failures = new Dictionary<string, string[]>();
    }
    public ValidationException(IList<ValidationFailure> failures)
        : this()
    { 
        var propertyNames = failures.Select(e => e.PropertyName).Distinct();
        foreach (var propertyName in propertyNames) {
            var property = System.Text.Json.JsonNamingPolicy.CamelCase.ConvertName(propertyName);
            var propertyFailures = failures.Where(e => e.PropertyName == propertyName).Select(e => e.ErrorMessage).ToArray();
            Failures.Add(property, propertyFailures);
        }
    }
    public IDictionary<string, string[]> Failures { get; }
}
