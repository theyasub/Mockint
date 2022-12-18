using AbuInt.Data.DbContexts;
using AbuInt.Domain.Entities.Chats;
using AbuInt.Domain.Entities.Commons;
using AbuInt.Domain.Entities.Companies;
using AbuInt.Domain.Entities.Quizes;
using AbuInt.Domain.Entities.Users;

namespace AbuInt.Data.IRepositories;

public interface IUnitOfWork : IDisposable
{
    #region Commons
    public IRepository<Asset> Assets { get; }
    #endregion

    #region Companies
    public IRepository<Company> Companies { get; }
    public IRepository<Interview> Interviews { get; }
    public IRepository<Vacancy> Vacancies { get; }
    #endregion

    #region Users
    public IRepository<User> Users { get; }
    public IRepository<Experience> Experiences { get; }
    public IRepository<UserDetail> UserDetails { get; }
    #endregion

    #region Quizes (optional)
    public IRepository<Question> Questions { get; }
    public IRepository<QuestionAnswer> QuestionAnswers { get; }
    public IRepository<Quize> Quizes { get; }
    public IRepository<QuizeResult> QuizeResults { get; }
    #endregion

    public AbuIntDbContext DbContext { get; }

    public Task<int> SaveChangesAsync();
}
