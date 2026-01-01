using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mymvc.Models.Process;
using mymvc.Models.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
namespace mymvc.Controllers;
public class RoleController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleController(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }
    [Authorize(Policy =nameof(SystemPermissions.RoleView))]
    public async Task<IActionResult> Index()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        return View(roles);
    }
    [Authorize(Policy =nameof(SystemPermissions.RoleCreate))]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(string roleName)
    {
        if (!string.IsNullOrEmpty(roleName))
        {
            var role = new IdentityRole(roleName.Trim());
            await _roleManager.CreateAsync(role);
        }
        return RedirectToAction("Index");
    }
    // Giao diện chỉnh sửa
    [Authorize(Policy =nameof(SystemPermissions.RoleEdit))]
    public async Task<IActionResult> Edit(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
        {
            return NotFound();
        }
        return View(role);
    }

    // Xử lý chỉnh sửa
    [HttpPost]
    public async Task<IActionResult> Edit(string id, string newName)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
        {
            return NotFound();
        }
        role.Name = newName;
        await _roleManager.UpdateAsync(role);
        return RedirectToAction("Index");
    }


    // Giao diện chỉnh sửa
    [Authorize(Policy =nameof(SystemPermissions.RoleDelete))]
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null)
            {
                return NotFound();
            }

             var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
    }
    // Xử lý xóa vai trò
    [HttpPost,ActionName("Delete")]
    public async Task<IActionResult> Deleteconfirmed(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role != null)
        {
            await _roleManager.DeleteAsync(role);
        }
        return RedirectToAction("Index");
    }
        [Authorize(Policy =nameof(SystemPermissions.AssignClaim))]
        public async Task<IActionResult> AssignClaim(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return BadRequest();
            }

            // Lấy toàn bộ danh sách quyền từ Enum SystemPermissions
            var allPermissions = Enum.GetValues(typeof(SystemPermissions))
                .Cast<SystemPermissions>()
                .Select(p => p.ToString())
                .ToList();

            // Lấy danh sách Claims hiện có của Role này
            var roleClaims = await _roleManager.GetClaimsAsync(role);
            if (roleClaims == null)
            {
                roleClaims = new List<Claim>();
            }

            var model = new RoleClaimVM
            {
                RoleId = role.Id,
                RoleName = role.Name,
                Claims = allPermissions.Select(p => new RoleClaim
                {
                    Type = "Permission",
                    Value = p,
                    // Kiểm tra xem quyền này đã được gán cho Role chưa
                    Selected = roleClaims.Any(c => c.Type == "Permission" && c.Value == p)
                }).ToList()
            };

            return View(model);
        }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AssignClaim(RoleClaimVM model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var role = await _roleManager.FindByIdAsync(model.RoleId);
        if (role == null)
        {
            return BadRequest();
        }

        // Lấy danh sách Claims hiện tại để xóa các Permission cũ
        var claims = await _roleManager.GetClaimsAsync(role);
        if (claims == null)
        {
            claims = new List<Claim>();
        }

        // Xóa toàn bộ Claims cũ loại "Permission" để cập nhật mới
        foreach (var claim in claims.Where(c => c.Type == "Permission"))
        {
            await _roleManager.RemoveClaimAsync(role, claim);
        }

        // Thêm các Claims mới mà người dùng đã chọn (Selected == true)
        foreach (var claim in model.Claims.Where(c => c.Selected))
        {
            await _roleManager.AddClaimAsync(role, new Claim(claim.Type, claim.Value));
        }

        return RedirectToAction(nameof(Index));
    }
}