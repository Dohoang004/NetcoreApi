using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mymvc.Models;
using mymvc.Models.ViewModels;

namespace VicemMVCIdentity.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Hiển thị danh sách người dùng kèm vai trò của họ
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var usersWithRoles = new List<UserWithRoleVM>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                usersWithRoles.Add(new UserWithRoleVM { User = user, Roles = roles.ToList() });
            }
            return View(usersWithRoles);
        }
        // Chuẩn bị dữ liệu để gán vai trò
        public async Task<IActionResult> AssignRole(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId); //
            if (user == null) return NotFound(); //

            var userRoles = await _userManager.GetRolesAsync(user); //
            var allRoles = await _roleManager.Roles.Select(r => new RoleVM { Id = r.Id, Name = r.Name }).ToListAsync(); //

            var viewModel = new AssignRoleVM
            {
                UserId = userId, //
                AllRoles = allRoles, //
                SelectedRoles = userRoles //
            };

            return View(viewModel); //
        }
        // POST: Xử lý gán vai trò
    [HttpPost]
    public async Task<IActionResult> AssignRole(AssignRoleVM model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null) return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);
            
            // Thêm các vai trò mới được chọn
            foreach (var role in model.SelectedRoles)
            {
                if (!userRoles.Contains(role))
                {
                    await _userManager.AddToRoleAsync(user, role);
                }
            }

            // Gỡ bỏ các vai trò không còn được chọn
            foreach (var role in userRoles)
            {
                if (!model.SelectedRoles.Contains(role))
                {
                    await _userManager.RemoveFromRoleAsync(user, role);
                }
            }
            return RedirectToAction("Index", "Account");
        }
        return View(model);
    }
    }
}