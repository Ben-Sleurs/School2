using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCGroentenEnFruit.Data;
using MVCGroentenEnFruit.Models.ViewModels;

namespace MVCGroentenEnFruit.Controllers
{
    public class AccountController : Controller
    {
        AppDbContext _context;
        UserManager<IdentityUser> _userManager;
        public AccountController(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        #region register
        [HttpGet]
        public IActionResult Register()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                if (registerViewModel.RoleId != null)
                {
                    var identityUser = new IdentityUser();
                    identityUser.Email = registerViewModel.Email;
                    identityUser.UserName = registerViewModel.Email;
                    var identityResult = await _userManager.CreateAsync(identityUser, registerViewModel.Passwoord);
                    if (identityResult.Succeeded)
                    {
                        var roleResult = await _userManager.AddToRoleAsync(identityUser,registerViewModel.RoleId);
                        if (roleResult.Succeeded)
                        {
                            return View("Login");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Problemen met toekennen van rol!");
                            return View();
                        }
                    }
                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Geen rol geselecteerd!");
                }
            }
            return View();
        }
        #endregion
        #region login

        #endregion
        #region logout

        #endregion
    }
}
