using TechInventory.Models;

namespace TechInventory.Data.Repository;

public interface IRepository<T> where T : class
{
    public Task<Result<T>> CreateAsync(T Entity);
    public Result<T> Update(T Entity);
    public Task<Result<T>> DeleteAsync(int Id);

    public Task<T?> GetAsync(int Id);
    public Task<List<T>> GetAllAsync();
}