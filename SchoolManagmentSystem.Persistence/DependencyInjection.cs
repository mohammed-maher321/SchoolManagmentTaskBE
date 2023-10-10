using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagmentSystem.Contract.IRepositories;
using SchoolManagmentSystem.Persistence.Repositories;

namespace SchoolManagmentSystem.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SchoolDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SchoolDbContext")));


        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        return services;
    }
}
