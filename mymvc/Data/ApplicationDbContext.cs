using Microsoft.EntityFrameworkCore;
using mymvc.Models;
namespace mymvc.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Person> Person { get; set;}
        public DbSet<Employee> Employee { get; set;}
        public DbSet<Hethongphanphoi> Hethongphanphoi { get; set;}
        //public DbSet<mymvc.Models.Daily> Daily { get; set; } = default!;
        
    }
}