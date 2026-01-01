using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mymvc.Models;
namespace mymvc.Controllers;
public class RoleController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleController(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<IActionResult> Index()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        return View(roles);
    }
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
}