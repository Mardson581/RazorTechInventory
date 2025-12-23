using TechInventory.Models;

namespace TechInventory.Services.Maintenance;

public interface IMaintenanceRecordService
{
    public Task<Result<bool>> CreateRecord(MaintenanceRecord record);
    public Task<Result<bool>> UpdateRecord(MaintenanceRecord record);
    public Task<Result<bool>> DeleteRecord(int id);

    public Task<MaintenanceRecord> GetRecordById(int id);
    public Task<List<MaintenanceRecord>> GetAllRecords();
    public Task<List<MaintenanceRecord>> GetAllRecordsByDevice(Models.Device device);
    
    public Task<Result<bool>> CheckIncludes(MaintenanceRecord record);
}