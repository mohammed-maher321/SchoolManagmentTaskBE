using Microsoft.Extensions.DependencyInjection;
using SchoolManagmentSystem.Application.Services;
using SchoolManagmentSystem.Application.Validators;
using SchoolManagmentSystem.Contract.IServices;

namespace SchoolManagmentSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IStudentService, StudentService>();

        services.AddScoped<CourseValidator>();
        services.AddScoped<StudentValidator>();
        return services;
    }
}