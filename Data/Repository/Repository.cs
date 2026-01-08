using Microsoft.EntityFrameworkCore;
using TechInventory.Data.Context;
using TechInventory.Models;
using System.Linq.Expressions;

namespace TechInventory.Data.Repository;

public class Repository<T>(InventoryDbContext context) : IRepository<T> where T : class
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

    public async Task<List<T>> GetWhere(Expression<Func<T, bool>> filter, string[] includes = null)
    {
        IQueryable<T> query = _set.AsNoTracking();

        if (filter != null)
            query = _set.Where(filter);
        
        if (includes != null)
            foreach (string property in includes)
            {
                query = query.Include(property.Trim());
            }

        return await query.ToListAsync();
    }

    public async Task<List<T>> GetWhere(Expression<Func<T, bool>> filter, string includes)
    {
        return await GetWhere(filter, new[] { includes });
    }

    public async Task<T?> FirstOrDefault(Expression<Func<T, bool>> filter, string[] includes = null)
    {
        IQueryable<T> query = _set.AsNoTracking();

        if (includes != null)
        {
            foreach (string property in includes)
            {
                query = query.Include(property.Trim());
            }
        }
                
        return await query.FirstOrDefaultAsync(filter);
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
            return Result<bool>.Failure($"Não foi possível salvar as informações {ex.Message}", false);
        }
    }
}