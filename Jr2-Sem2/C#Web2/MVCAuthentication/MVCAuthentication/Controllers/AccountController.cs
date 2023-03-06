using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCAuthentication.ViewModels;
using System.Runtime.CompilerServices;

namespace MVCAuthentication.Controllers
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

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = model.UserName;
                user.Email = model.Email;
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.USER_ROLE);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                    return View(model);
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(UserViewModel model)
        {            
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var searchUser = await _userManager.FindByNameAsync(model.UserName);
                    if (searchUser != null)
                        await _signInManager.SignInAsync(searchUser, isPersistent: false);
                    return RedirectToAction("Index", "Test");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt");
                }
            }
            return View("Login", model);
        }
        [HttpGet]
        public IActionResult FacebookLogin()
        {
            string redirectUrl = Url.Action("FacebookResponse", "Account");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", redirectUrl);
            return new ChallengeResult("Facebook", properties);
        }
        public async Task<IActionResult> FacebookResponse()
        {
            ExternalLoginInfo externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync();
            if (externalLoginInfo == null)
            {
                return RedirectToAction(nameof(Login));
            }
            return View("login");
        }
        public async Task<IActionResult> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return View("Login");
        }
    }
}
