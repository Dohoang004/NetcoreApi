using Microsoft.EntityFrameworkCore;
using mymvc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace mymvc.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
        {
            
        }
        public DbSet<Person> Person { get; set;}
        public DbSet<Employee> Employee { get; set;}
        public DbSet<Hethongphanphoi> Hethongphanphoi { get; set;}
        //public DbSet<mymvc.Models.Daily> Daily { get; set; } = default!;
        
    }
    

}