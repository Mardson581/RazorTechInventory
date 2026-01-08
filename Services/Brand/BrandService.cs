using TechInventory.Models;
using TechInventory.Data.UnitOfWork;
using TechInventory.Data.Repository;

namespace TechInventory.Services.Brand;

public class BrandService : IBrandService, IDisposable
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IRepository<Models.Brand> _repository;
    private bool _disposed = false;
    
    public BrandService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = (UnitOfWork)unitOfWork;
        _repository = _unitOfWork.BrandRepository;
    }

    public async Task<Result<bool>> CreateBrand(Models.Brand brand)
    {
        await _repository.CreateAsync(brand);
        return await _unitOfWork.CommitAsync();
    }

    public Task<Models.Brand> GetBrandById(int id)
    {
        return _repository.GetAsync(id);
    }

    public async Task<Models.Brand> GetBrandByName(string name)
    {
        return await _repository.FirstOrDefault(brand => brand.Name == name);
    }

    public Task<List<Models.Brand>> GetAllBrands()
    {
        return _repository.GetAllAsync();
    }

    public async Task<Result<bool>> UpdateBrand(Models.Brand brand)
    {
        _repository.Update(brand);
        return await _unitOfWork.CommitAsync();
    }
    
    public async Task<Result<bool>> DeleteBrand(int id)
    {
        var brand = await _repository.GetAsync(id);
        if (brand == null)
            return Result<bool>.Failure($"Marca com Id {id} não foi encontrada.", false);

        var deleteResult = await _repository.DeleteAsync(id);
        if (!deleteResult.IsSuccessful)
            return Result<bool>.Failure(deleteResult.Message, false);

        return await _unitOfWork.CommitAsync();
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