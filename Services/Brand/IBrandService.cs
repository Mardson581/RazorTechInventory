using TechInventory.Models;

namespace TechInventory.Services.Brand;

public interface IBrandService
{
    public Task<Result<bool>> CreateBrand(Models.Brand brand);
    public Task<Models.Brand> GetBrandById(int id);
    public Task<List<Models.Brand>> GetAllBrands();
    public Task<Models.Brand> GetBrandByName(string name);
    public Task<Result<bool>> UpdateBrand(Models.Brand brand);
    public Task<Result<bool>> DeleteBrand(int id);
}