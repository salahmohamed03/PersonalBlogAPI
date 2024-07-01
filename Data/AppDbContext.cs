using Microsoft.EntityFrameworkCore;
using Personal_Blog.Models;

namespace Personal_Blog.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<Blog> Blogs { get; set; }
	}
}
