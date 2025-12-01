using TechInventory.Models;

namespace TechInventory.Data.Repository;

public interface IRepository<T> where T : class
{
    public Task<Result<T>> Create(T Entity);
    public Task<Result<T>> Update(T Entity);
    public Task<Result<T>> Delete(int Id);
    public Task<Result<T>> Get(int Id);
    public Task<Result<T>> GetAll();
}