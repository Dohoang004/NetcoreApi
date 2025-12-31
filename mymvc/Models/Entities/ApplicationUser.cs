using Microsoft.AspNetCore.Identity;
namespace mymvc.Models;
public class ApplicationUser : IdentityUser
{
    [PersonalData]
    public string? FullName { get; set;}
}
