using TechInventory.Models;
using TechInventory.Data.UnitOfWork;
using TechInventory.Data.Repository;

namespace TechInventory.Services.Brand;

public class BrandService(UnitOfWork unitOfWork) : IBrandService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IRepository<Models.Brand> _repository = unitOfWork.BrandRepository;

    public async Task<Result<bool>> CreateBrand(Models.Brand brand)
    {
        await _repository.CreateAsync(brand);
        return await _unitOfWork.CommitAsync();
    }

    public Task<Models.Brand> GetBrandById(int id)
    {
        return _repository.GetAsync(id);
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
        await _repository.DeleteAsync(id);
        return await _unitOfWork.CommitAsync();
    }
}