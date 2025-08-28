using Microsoft.EntityFrameworkCore;
using mymvc.Models;
namespace mymvc.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Person> Person { get; set;}
        public DbSet<mymvc.Models.Employee> Employee { get; set; } = default!;
    }
}