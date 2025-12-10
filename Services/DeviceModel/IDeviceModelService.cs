using TechInventory.Models;

namespace TechInventory.Services.DeviceModel;

public interface IDeviceModelService
{
    public Task<Result<bool>> CreateDeviceModel(Models.DeviceModel deviceModel);
    public Task<Models.DeviceModel> GetDeviceModelById(int id);
    public Task<List<Models.DeviceModel>> GetAllDeviceModels();
    public Task<Result<bool>> UpdateDeviceModel(Models.DeviceModel deviceModel);
    public Task<Result<bool>> DeleteDeviceModel(int id);
    public Task<List<Models.DeviceModel>> GetDeviceModelsByBrandId(int brandId);

    public Task<Result<bool>> CheckIncludes(Models.DeviceModel deviceModel);
}