//cpfp twid kuzk sltr
using Microsoft.EntityFrameworkCore;
using mymvc.Data;
using mymvc.Models;
using mymvc.Models.Process;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Cấu hình MailSettings từ appsettings.json
builder.Services.AddOptions();
var mailSettings = builder.Configuration.GetSection("MailSettings");
builder.Services.Configure<MailSettings>(mailSettings);

// Đăng ký dịch vụ SendMailService
builder.Services.AddTransient<IEmailSender, SendMailService>();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

builder.Services.AddIdentity<ApplicationUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders() // Cần thiết cho Password/Email Reset
.AddDefaultUI();
builder.Services.AddRazorPages();
//


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
    //Config Password
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = false;
    // Config Login
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    // Config User
    options.User.RequireUniqueEmail = true;
});

// Application Cookie Configuration
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    // chi gui Cooke qua HTTPS
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

    // Giam thieu rui ro CSRF
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

// Data Protection Configuration
builder.Services.AddDataProtection()
    // chi dinh thu muc luu tru khoa bao ve du lieu
    .PersistKeysToFileSystem(new DirectoryInfo(@"./keys"))
    // xac dinh ten ung dung su dung dich vu bao ve du lieu
    .SetApplicationName("YourAppName")
    // dat thoi gian so cho khoa bao mat du lieu
    .SetDefaultKeyLifetime(TimeSpan.FromDays(14));


builder.Services.AddAuthorization(options =>
{
    /*
    // Yêu cầu đặc quyền Role có giá trị AdminOnly
    options.AddPolicy("Role", policy => policy.RequireClaim("Role", "AdminOnly"));
    
    // Yêu cầu đặc quyền Role có giá trị MemberOnly
    options.AddPolicy("Permission", policy => policy.RequireClaim("Role", "MemberOnly"));
    options.AddPolicy("PolicyAdmin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("PolicyMember", policy => policy.RequireRole("Member"));
    options.AddPolicy("PolicyByPhoneNumber", policy => policy.Requirements.Add(new PolicyByPhoneNumberRequirement()));
*/
    // Tự động tạo Policy từ Enum SystemPermissions
    
    foreach (var permission in Enum.GetValues(typeof(SystemPermissions)).Cast<SystemPermissions>())
    {
        options.AddPolicy(permission.ToString(), policy =>
            policy.RequireClaim("Permission", permission.ToString()));
    }
});
builder.Services.AddSingleton<IAuthorizationHandler, PolicyByPhoneNumberHandler>();


builder.Services.AddTransient<MemberUnitSeeder>();
//builder.Services.AddTransient<StaffSeeder>();
var app = builder.Build();



// Thực hiện Seed dữ liệu khi ứng dụng khởi chạy

/*using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var seeder = services.GetRequiredService<MemberUnitSeeder>();
    seeder.SeedMemberUnits(300);
}*/
/*using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var seeder = services.GetRequiredService<StaffSeeder>();
    seeder.SeedStaffs(300);
}*/

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
