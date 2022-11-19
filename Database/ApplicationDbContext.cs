using CrudRazorPages.Model;
using Microsoft.EntityFrameworkCore;

namespace CrudRazorPages.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Category { get; set; }
    }
}
