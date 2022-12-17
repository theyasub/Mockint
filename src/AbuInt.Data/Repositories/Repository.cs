using AbuInt.Data.DbContexts;
using AbuInt.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AbuInt.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    private readonly AbuIntDbContext dbContext;
    private readonly DbSet<TEntity> dbSet;
    public Repository(AbuIntDbContext dbContext)
    {
        this.dbContext = dbContext;
        dbSet = dbContext.Set<TEntity>();
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        var entry = await dbSet.AddAsync(entity);

        return entry.Entity;
    }

    public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        var user = await GetAsync(expression);

        if (user is null)
            return false;

        dbSet.Remove(user);

        return true;
    }

    public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression = null, string[] includes = null, bool isTracking = true)
    {
        IQueryable<TEntity> query = expression is null ? dbSet : dbSet.Where(expression);

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        if (!isTracking)
            query = query.AsNoTracking();

        return query;
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, string[] includes = null)
        => await GetAll(expression, includes).FirstOrDefaultAsync();

    public async Task<TEntity> UpdateAsync(TEntity entity)
        => dbSet.Update(entity).Entity;
}
