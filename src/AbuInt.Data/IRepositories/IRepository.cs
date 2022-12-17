using System.Linq.Expressions;

namespace AbuInt.Data.IRepositories;

public interface IRepository<TEntity>
    where TEntity : class
{
    IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression = null, string[] includes = null, bool isTracking = true);
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, string[] includes = null);
    Task<TEntity> CreateAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression);
}
