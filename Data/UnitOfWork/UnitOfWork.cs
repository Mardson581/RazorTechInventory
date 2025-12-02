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

    public async Task<Result<int>> CommitAsync()
    {
        try
        {
            int updatesMade = await _context.SaveChangesAsync();
            return Result<int>.Success(updatesMade);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "UnitOfWork::Commit falhou");
            return Result<int>.Failure("Não foi possível salvar as informações (erro interno)", 0);
        }
    }
}