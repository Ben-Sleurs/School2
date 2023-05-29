using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PeopleApp.Data;

namespace PeopleApp.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;

    private readonly SignInManager<IdentityUser> _signinManager;

    private MemoryDbContext _context;

    public AccountController(MemoryDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signinManager)
    {
        _context = context;
        _userManager = userManager;
        _signinManager = signinManager;
    }

    public IActionResult Login()
    {
        CreateUserData();
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> TestUserLogin()
    {
        var result = await _signinManager.PasswordSignInAsync("test@test.be", "Test1234!",false,false);
        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }

        throw new Exception("Something wnet wrong when signing in the test user");
    }

    private void CreateUserData()
    {
        _context.Database.EnsureCreated();
        if (!_userManager.Users.Any())
        {
            var pwd = "Test1234!";
            var user = new IdentityUser
            {
                Email = "test@test.be"
            };
            user.UserName = user.Email;
            _userManager.CreateAsync(user, pwd);
        }

    }


}