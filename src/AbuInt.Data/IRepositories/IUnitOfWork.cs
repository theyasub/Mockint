using AbuInt.Data.DbContexts;

namespace AbuInt.Data.IRepositories;

public interface IUnitOfWork : IDisposable
{
    public AbuIntDbContext DbContext { get; }

    public Task<int> SaveChangesAsync();
}
