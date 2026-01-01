
using Bogus;
using mymvc.Data;

namespace mymvc.Models.Process
{
    public class StaffSeeder
    {
        private readonly ApplicationDbContext _context;

        public StaffSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SeedStaffs(int n)
        {
            var employees = GenerateStaffs(n);

            _context.Staff.AddRange(employees);
            _context.SaveChanges();
        }

        private List<Staff> GenerateStaffs(int n)
        {
            var faker = new Faker<Staff>()
                .RuleFor(e => e.FirstName, f => f.Name.FirstName())
                .RuleFor(e => e.LastName, f => f.Name.LastName())
                .RuleFor(e => e.Address, f => f.Address.FullAddress())
                .RuleFor(e => e.DateOfBirth, f => f.Date.Past(30, DateTime.Now.AddYears(-20)))
                .RuleFor(e => e.Position, f => f.Name.JobTitle())
                .RuleFor(e => e.Email, (f, e) => f.Internet.Email(e.FirstName, e.LastName))
                .RuleFor(e => e.HireDate, f => f.Date.Past(10));

            return faker.Generate(n);
        }
    }
}