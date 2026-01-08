using TechInventory.Models;
using TechInventory.Data.Context;
using TechInventory.Data.Repository;

namespace TechInventory.Services.Brand;

public class BrandService(InventoryDbContext context) : IBrandService
{
    private readonly IRepository<Models.Brand> _repository = new Repository<Models.Brand>(context);

    public async Task<Result<bool>> CreateBrand(Models.Brand brand)
    {
        await _repository.CreateAsync(brand);
        return await _repository.CommitAsync();
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
        return await _repository.CommitAsync();
    }
    
    public async Task<Result<bool>> DeleteBrand(int id)
    {
        var brand = await _repository.GetAsync(id);
        if (brand == null)
            return Result<bool>.Failure($"Marca com Id {id} não foi encontrada.", false);

        var deleteResult = await _repository.DeleteAsync(id);
        if (!deleteResult.IsSuccessful)
            return Result<bool>.Failure(deleteResult.Message, false);

        return await _repository.CommitAsync();
    }
}