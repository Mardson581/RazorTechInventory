using TechInventory.Data.UnitOfWork;
using TechInventory.Data.Repository;
using TechInventory.Models;

namespace TechInventory.Services.DeviceModel;

public class DeviceModelService : IDeviceModelService, IDisposable
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IRepository<Models.DeviceModel> _deviceModelRepository;
    private readonly IRepository<Models.Brand> _brandRepository;
    private bool _disposed = false;
    
    public DeviceModelService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = (UnitOfWork)unitOfWork;
        _deviceModelRepository = _unitOfWork.DeviceModelRepository;
        _brandRepository = _unitOfWork.BrandRepository;
    }

    public async Task<Result<bool>> CreateDeviceModel(Models.DeviceModel deviceModel)
    {
        var checkResult = await CheckIncludes(deviceModel);
        if (!checkResult.IsSuccessful)
            return checkResult;
            
        await _deviceModelRepository.CreateAsync(deviceModel);
        return await _unitOfWork.CommitAsync();
    }

    public async Task<Models.DeviceModel> GetDeviceModelById(int id)
    {
        return await _deviceModelRepository.GetAsync(id);
    }

    public async Task<List<Models.DeviceModel>> GetAllDeviceModels()
    {
        return await _deviceModelRepository.GetWhere(null, "Brand");
    }

    public async Task<Result<bool>> UpdateDeviceModel(Models.DeviceModel deviceModel)
    {
        var checkResult = await CheckIncludes(deviceModel);
        if (!checkResult.IsSuccessful)
            return checkResult;

        _deviceModelRepository.Update(deviceModel);
        return await _unitOfWork.CommitAsync();
    }

    public async Task<Result<bool>> DeleteDeviceModel(int id)
    {
        var model = await _deviceModelRepository.GetAsync(id);
        if (model is null)
            return Result<bool>.Failure($"Modelo de dispositivo com Id {id} não foi encontrado", false);

        var deleteResult = await _deviceModelRepository.DeleteAsync(id);
        if (!deleteResult.IsSuccessful)
            return Result<bool>.Failure(deleteResult.Message, false);

        return await _unitOfWork.CommitAsync();
    }

    public async Task<List<Models.DeviceModel>> GetDeviceModelsByBrandId(int brandId)
    {
        return await _deviceModelRepository.GetWhere(m => m.BrandId == brandId);
    }

    public async Task<Result<bool>> CheckIncludes(Models.DeviceModel deviceModel)
    {
        var brand = await _brandRepository.GetAsync(deviceModel.BrandId);

        if (brand == null) 
            return Result<bool>.Failure($"A marca com id {deviceModel.BrandId} não existe", false);
        return Result<bool>.Success(true);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // Nothing to dispose here as per the request.
                // _unitOfWork is managed by DI container.
            }

            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}