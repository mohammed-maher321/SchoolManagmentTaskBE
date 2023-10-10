using Ardalis.Specification.EntityFrameworkCore;
using SchoolManagmentSystem.Contract.IRepositories;

namespace SchoolManagmentSystem.Persistence.Repositories;

public class BaseRepository<T> : RepositoryBase<T>, IRepository<T> where T : class
{
    public BaseRepository(SchoolDbContext dbContext) : base(dbContext)
    {
    }
}