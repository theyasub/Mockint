using AbuInt.Data.DbContexts;
using AbuInt.Data.IRepositories;
using AbuInt.Domain.Entities.Chats;
using AbuInt.Domain.Entities.Commons;
using AbuInt.Domain.Entities.Companies;
using AbuInt.Domain.Entities.Quizes;
using AbuInt.Domain.Entities.Users;

namespace AbuInt.Data.Repositories;

public class UnitOfWork : IUnitOfWork
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

    #region Chats
    public IRepository<Room> Rooms { get; }
    public IRepository<Participant> Participants { get; }
    public IRepository<Message> Messages { get; }
    #endregion

    public AbuIntDbContext DbContext { get; }

    public UnitOfWork(AbuIntDbContext dbContext)
    {
        DbContext = dbContext;

        #region Commons
        Assets = new Repository<Asset>(dbContext);
        #endregion

        #region Companies
        Companies = new Repository<Company>(dbContext);
        Interviews = new Repository<Interview>(dbContext);
        Vacancies = new Repository<Vacancy>(dbContext);
        #endregion

        #region Users
        Users = new Repository<User>(dbContext);
        Experiences = new Repository<Experience>(dbContext);
        UserDetails = new Repository<UserDetail>(dbContext);
        #endregion

        #region Quizes (optional)
        Questions = new Repository<Question>(dbContext);
        QuestionAnswers = new Repository<QuestionAnswer>(dbContext);
        Quizes = new Repository<Quize>(dbContext);
        QuizeResults = new Repository<QuizeResult>(dbContext);
        #endregion

        #region Chats
        Rooms = new Repository<Room>(dbContext);
        Participants = new Repository<Participant>(dbContext);
        Messages = new Repository<Message>(dbContext);
        #endregion
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async Task<int> SaveChangesAsync()
        => await DbContext.SaveChangesAsync();
}
