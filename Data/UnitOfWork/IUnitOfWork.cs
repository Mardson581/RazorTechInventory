using TechInventory.Data.Repository;
using TechInventory.Models;

namespace TechInventory.Data.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IRepository<Brand> BrandRepository { get; }
    IRepository<Device> DeviceRepository { get; }
    IRepository<DeviceModel> DeviceModelRepository { get; }
    Task<Result<bool>> CommitAsync();
}
