using TechInventory.Models;
using TechInventory.Data.UnitOfWork;
using TechInventory.Data.Repository;

namespace TechInventory.Services.Maintenance;

public class MaintenanceRecordService(UnitOfWork unitOfWork) : IMaintenanceRecordService
{
    private readonly UnitOfWork _unitOfWork = unitOfWork;
    private readonly IRepository<MaintenanceRecord> _repository = unitOfWork.MaintenanceRecordRepository;
    private readonly IRepository<Models.Device> _deviceRepository = unitOfWork.DeviceRepository;

    public async Task<Result<bool>> CreateRecord(MaintenanceRecord record)
    {
        var checkResult = await CheckIncludes(record);
        if (!checkResult.IsSuccessful)
            return checkResult;

        await _repository.CreateAsync(record);
        return await _unitOfWork.CommitAsync();
    }

    public async Task<Result<bool>> UpdateRecord(MaintenanceRecord record)
    {
        var checkResult = await CheckIncludes(record);
        if (!checkResult.IsSuccessful)
            return checkResult;

        _repository.Update(record);
        return await _unitOfWork.CommitAsync();
    }

    public async Task<Result<bool>> DeleteRecord(int id)
    {
        var device = await _repository.GetAsync(id);
        if (device == null)
            return Result<bool>.Failure($"O dispositivo com Id {id} não foi encontrado", false);

        var deleteResult = await _repository.DeleteAsync(id);
        if (!deleteResult.IsSuccessful)
            return Result<bool>.Failure(deleteResult.Message, false);

        return await _unitOfWork.CommitAsync();
    }

    public async Task<MaintenanceRecord> GetRecordById(int id)
    {
        return await _repository.FirstOrDefault(
            r => r.MaintenanceRecordId == id,
            new string[]{"Device"}
        );
    }

    public async Task<List<MaintenanceRecord>> GetAllRecords()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<List<MaintenanceRecord>> GetAllRecordsByDevice(Models.Device device)
    {
        return await _repository.GetWhere(record => record.DeviceId == device.DeviceId);
    }

    public async Task<Result<bool>> CheckIncludes(MaintenanceRecord record)
    {
        var device = await _deviceRepository.GetAsync(record.DeviceId);
        if (device == null)
            return Result<bool>.Failure("O dispositivo associado ao registro não existe", false);
        return Result<bool>.Success(true);
    }
}