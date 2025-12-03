using TechInventory.Models;

namespace TechInventory.Services.Device;

public interface IDeviceService
{
    public Task<Result<bool>> AddDevice(Models.Device device);
    public Task<Result<bool>> DeleteDevice(int id);
    public Task<Result<bool>> UpdateDevice(Models.Device device);
    public Task<Models.Device> GetDeviceById(int id);
    public Task<Models.Device> GetDeviceByName(string name);

    public Task<List<Models.Device>> GetAllDevices();
    public Task<List<Models.Device>> GetAllDevicesWithStatus(DeviceStatus status);
    public Task<List<Models.Device>> GetAllDevicesByBrand(Models.Brand brand);
}