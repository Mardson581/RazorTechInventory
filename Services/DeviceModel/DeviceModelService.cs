using TechInventory.Data.UnitOfWork;
using TechInventory.Data.Repository;
using TechInventory.Data.Context;
using TechInventory.Models;

namespace TechInventory.Services.DeviceModel;

public class DeviceModelService : IDeviceModelService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IRepository<Models.DeviceModel> _repository;
    private readonly IRepository<Models.Brand> _brandRepository;

    public DeviceModelService(InventoryDbContext context)
    {
        _unitOfWork = new UnitOfWork(context);
        _repository = _unitOfWork.DeviceModelRepository;
        _brandRepository = _unitOfWork.BrandRepository;
    }

    public async Task<Result<bool>> CreateDeviceModel(Models.DeviceModel deviceModel)
    {
        var checkResult = await CheckIncludes(deviceModel);
        if (!checkResult.IsSuccessful)
            return checkResult;
            
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
        var checkResult = await CheckIncludes(deviceModel);
        if (!checkResult.IsSuccessful)
            return checkResult;

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

    public async Task<Result<bool>> CheckIncludes(Models.DeviceModel deviceModel)
    {
        var brand = await _brandRepository.GetAsync(deviceModel.BrandId);

        if (brand == null) 
            return Result<bool>.Failure($"A marca com id {deviceModel.BrandId} não existe", false);
        return Result<bool>.Success(true);
    }
}