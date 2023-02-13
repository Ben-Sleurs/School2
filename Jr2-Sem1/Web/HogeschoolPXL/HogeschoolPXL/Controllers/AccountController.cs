using HogeschoolPXL.Data;
using HogeschoolPXL.Data.DefaultData;
using HogeschoolPXL.Models.Data;
using HogeschoolPXL.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HogeschoolPXL.Controllers
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
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
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
        #region register
        [HttpGet]
        public IActionResult Register()
        {
            var temporaryRoles = _context.Roles.Where(x => x.Name.Contains("Temp"));
            ViewData["Role"] = temporaryRoles.Select(x => new SelectListItem()
            {
                Value = x.Id,
                Text = x.Name.Substring(4)
            });
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                if (registerViewModel.Role != null)
                {
                    var identityUser = new IdentityUser();
                    identityUser.Email = registerViewModel.Email;
                    identityUser.UserName = registerViewModel.Email;
                    var identityResult = await _userManager.CreateAsync(identityUser, registerViewModel.Password);
                    if (identityResult.Succeeded)
                    {
                        var identityRole = await _roleManager.FindByIdAsync(registerViewModel.Role);

                        var roleResult = await _userManager.AddToRoleAsync(identityUser, identityRole.Name);
                        await CreateNewTempGebruiker(registerViewModel,identityRole.Name);
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
            var temporaryRoles = _context.Roles.Where(x => x.Name.Contains("Temp"));
            ViewData["Role"] = temporaryRoles.Select(x => new SelectListItem()
            {
                Value = x.Id,
                Text = x.Name.Substring(4)
            });
            return View();
        }

        private async Task CreateNewTempGebruiker(RegisterViewModel registerViewModel,string role)
        {
            Gebruiker nieuweGebruiker = new Gebruiker {
                Voornaam=registerViewModel.Voornaam,
                Naam=registerViewModel.Naam,
                Email=registerViewModel.Email,
                Role=role

            };
            _context.Gebruiker.Add(nieuweGebruiker);
            _context.SaveChanges();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return View("Login");
        }
        #endregion
    }
}
