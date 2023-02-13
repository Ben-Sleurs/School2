using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCBibliotheekBENSLE.Data;
using MVCBibliotheekBENSLE.Data.DefaultData;
using MVCBibliotheekBENSLE.Models.Data;
using MVCBibliotheekBENSLE.Models.ViewModels;

namespace MVCBibliotheekBENSLE.Controllers
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
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index","Reservaties");
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
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await CreateUserAsync(registerViewModel);
                if (result == IdentityResult.Success)
                    return View("Login");
                else Errors(result);
            }
            return View();
        }
        private async Task<IdentityResult> CreateUserAsync(RegisterViewModel registerViewModel)
        {
            Gebruiker gebruiker = new Gebruiker();
            gebruiker.Voornaam = registerViewModel.Voornaam;
            gebruiker.Achternaam = registerViewModel.Achternaam;
            var identityUser = new IdentityUser();
            identityUser.Email = registerViewModel.Email;
            identityUser.UserName = registerViewModel.GebruikersNaam;
            
            var identityResult = await _userManager.CreateAsync(identityUser, registerViewModel.Wachtwoord);
            if (identityResult.Succeeded)
            {

                gebruiker.IdentityUserId = identityUser.Id;
                var roleResult = await _userManager.AddToRoleAsync(identityUser, Roles.BibGebruiker);
                _context.Gebruiker.Add(gebruiker);
                _context.SaveChanges();
            }
            return identityResult;
        }
        private void Errors(IdentityResult identityResult)
        {
            foreach (var error in identityResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}
