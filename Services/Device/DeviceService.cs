using TechInventory.Models;
using TechInventory.Data.Repository;
using TechInventory.Data.UnitOfWork;

namespace TechInventory.Services.Device;

public class DeviceService(UnitOfWork unitOfWork) : IDeviceService
{
    private readonly UnitOfWork _unitOfWork = unitOfWork;
    private readonly IRepository<Models.Device> _repository = unitOfWork.DeviceRepository;
    private readonly IRepository<Models.DeviceModel> _modelRepository = unitOfWork.DeviceModelRepository;

    public async Task<Result<bool>> AddDevice(Models.Device device)
    {
        var checkResult = await CheckIncludes(device);
        if (!checkResult.IsSuccessful)
            return checkResult;

        await _repository.CreateAsync(device);
        return await _unitOfWork.CommitAsync();
    }

    public async Task<Result<bool>> DeleteDevice(int id)
    {
        var device = await _repository.GetAsync(id);
        if (device is null)
            return Result<bool>.Failure($"Dispositivo com Id {id} não foi encontrado", false);

        var deleteResult = await _repository.DeleteAsync(id);
        if (!deleteResult.IsSuccessful)
            return Result<bool>.Failure(deleteResult.Message, false);

        var commitResult = await _unitOfWork.CommitAsync();

        return commitResult.IsSuccessful ? Result<bool>.Success(true) : Result<bool>.Failure(commitResult.Message, false);
    }

    public async Task<Result<bool>> UpdateDevice(Models.Device device)
    {
        var checkResult = await CheckIncludes(device);
        if (!checkResult.IsSuccessful)
            return checkResult;
            
        _repository.Update(device);
        return await _unitOfWork.CommitAsync();
    }

    public async Task<Models.Device?> GetDeviceById(int id)
    {
        return await _repository.GetAsync(id);
    }

    public async Task<Models.Device> GetDeviceByName(string name)
    {
        List<Models.Device> devices = await _repository.GetAllAsync();
        return devices.FirstOrDefault(d => d.Name == name);
    }

    public Task<List<Models.Device>> GetAllDevices()
    {
        return _repository.GetWhere(null, "Model");
    }

    public async Task<List<Models.Device>> GetAllDevicesWithStatus(DeviceStatus status)
    {
        return await _repository.GetWhere(d => d.Status == status);
    }

    public async Task<List<Models.Device>> GetAllDevicesByBrand(Models.Brand brand)
    {
        return await _repository.GetWhere(
            d => d.Model.BrandId == brand.BrandId,
            "Model"
        );
    }

    public async Task<Result<bool>> CheckIncludes(Models.Device device)
    {
        // Check if the device's model exists
        var result = await _modelRepository.GetAsync(device.DeviceModelId);

        if (result == null)
            return Result<bool>.Failure($"Modelo com Id {device.DeviceModelId} não foi encontrado", false);

        return Result<bool>.Success(true);
    }
}