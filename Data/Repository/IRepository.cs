using TechInventory.Models;
using System.Linq.Expressions;

namespace TechInventory.Data.Repository;

public interface IRepository<T> where T : class
{
    public Task<Result<T>> CreateAsync(T Entity);
    public Result<T> Update(T Entity);
    public Task<Result<T>> DeleteAsync(int Id);

    public Task<T?> GetAsync(int Id);
    public Task<List<T>> GetAllAsync();

    public Task<List<T>> GetWhere(Expression<Func<T, bool>> filter, string[] includes = null);
    public Task<List<T>> GetWhere(Expression<Func<T, bool>> filter, string includes);
    public Task<T?> FirstOrDefault(Expression<Func<T, bool>> filter, string[] includes = null);
    Task<Result<bool>> CommitAsync();
}