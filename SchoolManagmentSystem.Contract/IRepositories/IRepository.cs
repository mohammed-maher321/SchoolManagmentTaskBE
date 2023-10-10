using Ardalis.Specification;
namespace SchoolManagmentSystem.Contract.IRepositories;

public interface IRepository<T> : IRepositoryBase<T> where T : class
{
}

