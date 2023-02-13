using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCVoertuig.Models.ViewModels;

namespace MVCVoertuig.Controllers
{
    public class AccountController : Controller
    {
        UserManager<IdentityUser> _userManager;
        SignInManager<IdentityUser> _signInManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            var identityresult = await _signInManager.PasswordSignInAsync(login.Email,login.Password,false,lockoutOnFailure:false);
            if (identityresult.Succeeded)
            {
                return RedirectToAction("Index", "Voertuig");
            }
            ModelState.AddModelError("", "Probleem met inloggen");
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var identityUser = new IdentityUser();
                identityUser.Email = registerViewModel.Email;
                identityUser.UserName = registerViewModel.Email;
                var identityResult = await _userManager.CreateAsync(identityUser,registerViewModel.Password);
                if (identityResult.Succeeded)
                {
                    return View("RegistrationCompleted");
                }
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }
        public async Task<IActionResult> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

    }
}
