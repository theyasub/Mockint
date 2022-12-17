using AbuInt.Data.DbContexts;
using AbuInt.Data.IRepositories;

namespace AbuInt.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public AbuIntDbContext DbContext { get; }

    public UnitOfWork(AbuIntDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public void Dispose() 
    {
        GC.SuppressFinalize(this);
    }

    public async Task<int> SaveChangesAsync()
        => await DbContext.SaveChangesAsync();
}
