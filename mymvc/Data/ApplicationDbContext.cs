using Microsoft.EntityFrameworkCore;
using mymvc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using mymvc.Models.Entities;
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
        public DbSet<Staff> Staff { get; set;}
        public DbSet<MemberUnit> MemberUnit { get; set;}
        //public DbSet<mymvc.Models.Daily> Daily { get; set; } = default!;
        
    protected override void OnModelCreating(ModelBuilder builder)
{
    base.OnModelCreating(builder);

    builder.Entity<ApplicationUser>().ToTable("Users");
    builder.Entity<IdentityRole>().ToTable("Roles");
    builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
    builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
    builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
    builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
    builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
}
    }
    

}