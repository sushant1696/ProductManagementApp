
using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Model;

namespace ProductManagementAPI.DBContext
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<Product> tbl_products { get; set; }
	}
}
