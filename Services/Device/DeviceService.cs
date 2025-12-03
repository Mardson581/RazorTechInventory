using TechInventory.Models;
using TechInventory.Data.Repository;
using TechInventory.Data.UnitOfWork;

namespace TechInventory.Services.Device;

public class DeviceService(UnitOfWork unitOfWork) : IDeviceService
{
    private readonly UnitOfWork _unitOfWork = unitOfWork;
    private readonly IRepository<Models.Device> _repository = unitOfWork.DeviceRepository;

    public async Task<Result<bool>> AddDevice(Models.Device device)
    {
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
        return _repository.GetAllAsync();
    }

    public async Task<List<Models.Device>> GetAllDevicesWithStatus(DeviceStatus status)
    {
        List<Models.Device> devices = await _repository.GetAllAsync();
        return devices.Where(d => d.Status == status).ToList();
    }
}