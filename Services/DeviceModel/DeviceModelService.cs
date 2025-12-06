using TechInventory.Data.UnitOfWork;
using TechInventory.Data.Repository;
using TechInventory.Models;

namespace TechInventory.Services.DeviceModel;

public class DeviceModelService(UnitOfWork unitOfWork) : IDeviceModelService
{
    private readonly UnitOfWork _unitOfWork = unitOfWork;
    private readonly IRepository<Models.DeviceModel> _repository = unitOfWork.DeviceModelRepository;

    public async Task<Result<bool>> CreateDeviceModel(Models.DeviceModel deviceModel)
    {
        await _repository.CreateAsync(deviceModel);
        return await _unitOfWork.CommitAsync();
    }

    public async Task<Models.DeviceModel> GetDeviceModelById(int id)
    {
        return await _repository.GetAsync(id);
    }

    public async Task<List<Models.DeviceModel>> GetAllDeviceModels()
    {
        return await _repository.GetWhere(null, "Brand");
    }

    public async Task<Result<bool>> UpdateDeviceModel(Models.DeviceModel deviceModel)
    {
        _repository.Update(deviceModel);
        return await _unitOfWork.CommitAsync();
    }

    public Task<Result<bool>> DeleteDeviceModel(int id)
    {
        _repository.DeleteAsync(id);
        return _unitOfWork.CommitAsync();
    }

    public async Task<List<Models.DeviceModel>> GetDeviceModelsByBrandId(int brandId)
    {
        return await _repository.GetWhere(m => m.BrandId == brandId);
    }
}