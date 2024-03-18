using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MyApplication.Db;
using Microsoft.AspNetCore.Authorization;
using User_Roles_Application.Models;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    private readonly ApplicationDbContext _db;

    public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _db = db;
    }

    public IActionResult AdminPanel()
    {
        return View();
    }

    // GET
    public IActionResult CreateUser()
    {
        return View();
    }

    // POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateUser(User model)
    {
        if (ModelState.IsValid)
        {
            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, model.Role);
                return RedirectToAction("UserList");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(model);
    }

    public IActionResult UserList()
    {
        IEnumerable<IdentityUser> usersobjList = _userManager.Users.ToList();
        if (usersobjList == null)
        {
            return View();
        }
        return View(usersobjList);
    }

    public IActionResult TaskList()
    {
        IEnumerable<Tasks> objTasksList = _db.Tasks.ToList();
        if (objTasksList == null)
        {
            return NotFound();
        }
        return View(objTasksList);
    }

    //GET
    public async Task<IActionResult> Edit(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }
        var model = new User
        {
            Email = user.Email,
            Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault(),
        };
        return View(model);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(User model)
    {
        ModelState.Remove("Password");
        ModelState.Remove("ConfirmPassword");
        if (!ModelState.IsValid)
        {
            return NotFound();
        }
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return NotFound();
        }

        var roles = await _userManager.GetRolesAsync(user);

        await _userManager.RemoveFromRolesAsync(user, roles);

        await _userManager.AddToRoleAsync(user, model.Role);
        return RedirectToAction("UserList");
    }

    public async Task<IActionResult> Delete(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        var model = new User
        {
            Email = user.Email,
            Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault(),
        };
        return View(model);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(User model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return NotFound();
        }
        await _userManager.DeleteAsync(user);
        return RedirectToAction("UserList");
    }
}