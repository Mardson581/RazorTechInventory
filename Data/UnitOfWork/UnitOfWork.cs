using TechInventory.Data.Context;
using TechInventory.Models;
using TechInventory.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace TechInventory.Data.UnitOfWork;

public class UnitOfWork(InventoryDbContext context, ILogger<UnitOfWork> logger)
{
    private readonly InventoryDbContext _context = context;
    private readonly ILogger<UnitOfWork> _logger = logger;

    private IRepository<Device> deviceRepository;
    private IRepository<DeviceModel> deviceModelRepository;
    private IRepository<Brand> brandRepository;
    private IRepository<MaintenanceRecord> maintenanceRecordRepository;


    public IRepository<Device> DeviceRepository
    {
        get
        {
            if (deviceRepository == null)
                deviceRepository = new Repository<Device>(_context);
            return deviceRepository;
        }
    }

    public IRepository<DeviceModel> DeviceModelRepository
    {
        get
        {
            if (deviceModelRepository == null)
                deviceModelRepository = new Repository<DeviceModel>(_context);
            return deviceModelRepository;
        }
    }

    public IRepository<Brand> BrandRepository
    {
        get
        {
            if (brandRepository == null)
                brandRepository = new Repository<Brand>(_context);
            return brandRepository;
        }
    }

    public IRepository<MaintenanceRecord> MaintenanceRecordRepository
    {
        get
        {
            if (maintenanceRecordRepository == null)
                maintenanceRecordRepository = new Repository<MaintenanceRecord>(_context);
            return maintenanceRecordRepository;
        }
    }

    public async Task<Result<bool>> CommitAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            return Result<bool>.Success(true);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "UnitOfWork::Commit falhou");
            return Result<bool>.Failure("Não foi possível salvar as informações (erro interno)", false);
        }
    }
}