using Microsoft.EntityFrameworkCore;
using TechInventory.Data.Context;
using TechInventory.Models;

namespace TechInventory.Data.Repository;

public class Repository<T>(InventoryDbContext context)  : IRepository<T> where T : class
{
    private readonly InventoryDbContext _context = context;
    private readonly DbSet<T> _set = context.Set<T>();

    public async Task<Result<T>> CreateAsync(T Entity)
    {
        await _set.AddAsync(Entity);
        return Result<T>.Success(Entity);
    }
    public Result<T> Update(T Entity)
    {
        _set.Update(Entity);
        return Result<T>.Success(Entity);
    }

    public async Task<Result<T>> DeleteAsync(int Id)
    {
        T? entity = await GetAsync(Id);
        if (entity == null)
            return Result<T>.Failure($"O {typeof(T)} com ID {Id} não foi encontrado", null);

        _set.Remove(entity);
        return Result<T>.Success(entity);
    }

    public async Task<T?> GetAsync(int Id)
    {
        return await _set.FindAsync(new object[]{ Id });
    }

    public virtual async Task<List<T>> GetAllAsync()
    {
        return await _set.AsNoTracking().ToListAsync();
    }
}