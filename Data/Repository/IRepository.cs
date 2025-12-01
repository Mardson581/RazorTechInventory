using TechInventory.Models;

namespace TechInventory.Data.Repository;

public interface IRepository<T> where T : class
{
    public Task<Result<T>> CreateAsync(T Entity);
    public Task<Result<T>> UpdateAsync(T Entity);
    public Task<Result<T>> DeleteAsync(int Id);

    public Task<Result<T>> GetAsync(int Id);
    public Task<Result<T>> GetByNameAsync(string Name);
    public Task<IEnumerable<Device>> GetAllAsync();
    public Task<IEnumerable<Device>> GetAllByTypeAsync(DeviceType Type);
}