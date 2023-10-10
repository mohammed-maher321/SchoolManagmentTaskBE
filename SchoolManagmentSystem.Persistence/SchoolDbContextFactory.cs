using Microsoft.EntityFrameworkCore;

namespace SchoolManagmentSystem.Persistence;

public class SchoolDbContextFactory : DesignTimeDbContextFactoryBase<SchoolDbContext>
{
    protected override SchoolDbContext CreateNewInstance(DbContextOptions<SchoolDbContext> options)
    {
        return new SchoolDbContext(options);
    }
}
