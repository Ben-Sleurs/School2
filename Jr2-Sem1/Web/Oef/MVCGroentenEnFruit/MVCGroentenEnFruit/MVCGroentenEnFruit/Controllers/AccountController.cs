using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVCGroentenEnFruit.Data;
using MVCGroentenEnFruit.Models.ViewModels;

namespace MVCGroentenEnFruit.Controllers
{
    public class AccountController : Controller
    {
        AppDbContext _context;
        UserManager<IdentityUser> _userManager;
        SignInManager<IdentityUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;

        public AccountController(AppDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult AccessDenied()
        {
            return View();
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
                    var identityResult = await _userManager.CreateAsync(identityUser, registerViewModel.Password);
                    if (identityResult.Succeeded)
                    {
                        var identityRole = await _roleManager.FindByIdAsync(registerViewModel.RoleId);

                        var roleResult = await _userManager.AddToRoleAsync(identityUser, identityRole.Name);
                        if (roleResult.Succeeded)
                            return View("Login");
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
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel login)
        {
            var identityUser = await _userManager.FindByEmailAsync(login.Email);
            if (identityUser != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(identityUser.UserName, login.Password, false, false);
                if (signInResult.Succeeded)
                {
                    return RedirectToAction("index", "Home");
                }

            }          

            ModelState.AddModelError("", "Probleem met inloggen");
            return View();
        }
        #endregion
        #region logout

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return View("Login");
        }
        #endregion

        #region identity

        public IActionResult Identity()
        {
            var identityViewModel = new IdentityViewModel();
            identityViewModel.Roles = _roleManager.Roles;
            identityViewModel.Users = _userManager.Users;
            return View(identityViewModel);
        }

        #endregion
        #region Role
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public   async Task<IActionResult> CreateRole(RoleViewModel role)
        {
            if (!await _roleManager.RoleExistsAsync(role.RoleName))
            {
                var identityRole = new IdentityRole(role.RoleName);
                var result = await _roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                    return RedirectToAction("Identity");
                
               
            }
            ModelState.AddModelError("", "probleem met aanmaken van Rol");
            return View();
        }
        #endregion
        [HttpGet]
        public async Task<IActionResult> UserClaim()
        {
            var user = User;
            var identityUser = await _userManager.GetUserAsync(user);
            if (identityUser != null)
            {
                return View("UserClaim", identityUser);
            }
            return View("login");
        }
    }
}