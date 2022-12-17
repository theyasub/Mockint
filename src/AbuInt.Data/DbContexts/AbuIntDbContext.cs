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
		
}
