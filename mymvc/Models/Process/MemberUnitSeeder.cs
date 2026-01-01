using Bogus;
using mymvc.Data;
using mymvc.Models;
namespace mymvc.Models.Process
{
    public class MemberUnitSeeder
    {
        private readonly ApplicationDbContext _context;

        public MemberUnitSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SeedMemberUnits(int n)
        {
            var employee = GenerateMemberUnits(n);

            _context.MemberUnit.AddRange(employee);
            _context.SaveChanges();
        }

        private List<MemberUnit> GenerateMemberUnits(int n)
        {
            var faker = new Faker<MemberUnit>()
                .RuleFor(e => e.Name, f => f.Name.FullName())
                .RuleFor(e => e.Address, f => f.Address.FullAddress())
                .RuleFor(e => e.PhoneNumber, f => f.Phone.PhoneNumber("09#-###-####"))
                .RuleFor(e => e.WebsiteUrl, f => f.Internet.Url());

            return faker.Generate(n);
        }
    }
}