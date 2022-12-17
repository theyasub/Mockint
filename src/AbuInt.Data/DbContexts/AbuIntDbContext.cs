using AbuInt.Domain.Entities.Commons;
using AbuInt.Domain.Entities.Companies;
using AbuInt.Domain.Entities.Quizes;
using AbuInt.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AbuInt.Data.DbContexts;

public class AbuIntDbContext : DbContext
{
	public AbuIntDbContext(DbContextOptions<AbuIntDbContext> options)
		: base(options)
	{
		// Database.Migrate();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

    }

	#region Commons
	public virtual DbSet<Attachment> Attachments { get; set; }
    #endregion
	
    #region Companies
	public virtual DbSet<Company> Companies { get; set; }
	public virtual DbSet<Interview> Interviews { get; set; }
	public virtual DbSet<Vacancy> Vacancies { get; set; }
	#endregion
	
	#region Users
	public virtual DbSet<User> Users { get; set; }
	public virtual DbSet<Experience> Experiences { get; set; }
	public virtual DbSet<UserDetail> UserDetails { get; set; }
	#endregion

	#region Quizes (optional)
	public virtual DbSet<Question> Questions { get; set; }
	public virtual DbSet<QuestionAnswer> QuestionAnswers { get; set; }
	public virtual DbSet<Quize> Quizes { get; set; }
	public virtual DbSet<QuizeResult> QuizeResults { get; set; }
	#endregion

}
