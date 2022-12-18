using AbuInt.Domain.Entities.Chats;
using AbuInt.Domain.Entities.Commons;
using AbuInt.Domain.Entities.Companies;
using AbuInt.Domain.Entities.Quizes;
using AbuInt.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace AbuInt.Data.DbContexts;

public class AbuIntDbContext : DbContext
{
	public AbuIntDbContext(DbContextOptions<AbuIntDbContext> options)
		: base(options)
	{
		Database.Migrate();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<User>()
			.HasOne(x => x.UserDetail)
			.WithOne(x => x.User)
			.OnDelete(DeleteBehavior.NoAction);

		modelBuilder.Entity<Vacancy>()
			.HasOne(x => x.User)
			.WithMany(x => x.Vacancies)
			.OnDelete(DeleteBehavior.NoAction);

    modelBuilder.Entity<User>().Navigation(x => x.Image).AutoInclude();

	}

	#region Asset
	public DbSet<Asset> Assets { get; set; }
    #endregion

    #region Companies
    public DbSet<Company> Companies { get; set; }
	public DbSet<Interview> Interviews { get; set; }
	public DbSet<Vacancy> Vacancies { get; set; }
	#endregion

	#region Users
	public DbSet<User> Users { get; set; }
	public DbSet<Experience> Experiences { get; set; }
	public DbSet<UserDetail> UserDetails { get; set; }
    #endregion

    #region Chats 

	public DbSet<Room> Rooms { get; set; }
	public DbSet<Participant> Participants { get; set; }
	public DbSet<Message> Messages { get; set; }

    #endregion

    #region Quizes (optional)
    public DbSet<Question> Questions { get; set; }
	public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
	public DbSet<Quize> Quizes { get; set; }
	public DbSet<QuizeResult> QuizeResults { get; set; }
	#endregion
}
