using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCAuthentication.ViewModels;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

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
        [HttpPost]
        public async Task<IActionResult> FacebookResponse()
        {
            //retrieve information that was send in the http request (by facebook)
            ExternalLoginInfo externalLoginInfo =
                await _signInManager.GetExternalLoginInfoAsync();
            if (externalLoginInfo == null)
            {
                //user did not login properly with facebook -> redirect to login page
                return RedirectToAction(nameof(Login));
            }
            //Put info provided by facebook (claims) into a viewmodel
            string userName = externalLoginInfo.Principal.FindFirst(ClaimTypes.Name).Value;
            //make sure username is unique
            UserViewModel model = new UserViewModel()
            {
                UserName = userName,
                Email = externalLoginInfo.Principal.FindFirst(ClaimTypes.Email).Value
            };
            //try to sign in with facebook user id (ProviderKey)
            Microsoft.AspNetCore.Identity.SignInResult result =
                await _signInManager.ExternalLoginSignInAsync(
                externalLoginInfo.LoginProvider, externalLoginInfo.ProviderKey, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Test");
            }
            else
            {
                var identityResult = await CreateIdentityUserAsync(externalLoginInfo);
                if (identityResult.Succeeded)
                {
                    return RedirectToAction("Index", "Test");
                }
            }
            return View("login");
        }

        private async Task<IdentityResult> CreateIdentityUserAsync(ExternalLoginInfo externalLoginInfo)
        {
            //Put info provided by external provider (claims) into a viewmodel
            //Sign in failed -> user does not exist yet in our database -> create one
            IdentityUser user = GetIdentityUser(externalLoginInfo);
            IdentityResult identityResult = await _userManager.CreateAsync(user);
            if (identityResult.Succeeded)
            {
                //link the created user to the facebook login info
                identityResult = await _userManager.AddLoginAsync(
                user, externalLoginInfo);
                if (identityResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                }
                else
                {
                    return IdentityResult.Failed(
                    new IdentityError { Description = "error in AddLogin" });
                }
            }
            return identityResult;
        }
        private IdentityUser GetIdentityUser(ExternalLoginInfo info)
        {
            string userName = info.Principal.FindFirst(ClaimTypes.Name).Value;
            userName = $"{userName}_{info.LoginProvider}_{info.ProviderKey}";
            string email = info.Principal.FindFirst(ClaimTypes.Email).Value;
            IdentityUser user = new IdentityUser(userName)
            {
                Email = email
            };
            return user;
        }

        public IActionResult GoogleLogin()
        {
            string redirectUrl = Url.Action("GoogleResponse", "Account");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }

        public async Task<IActionResult> GoogleResponse()
        {
            ExternalLoginInfo externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync();
            if (externalLoginInfo == null)
            {
                return RedirectToAction(nameof(Login));
            }

            //Put info provided by google (claims) into a viewmodel
            string userName = externalLoginInfo.Principal.FindFirst(ClaimTypes.Name).Value;
            //make sure username is unique
            UserViewModel model = new UserViewModel()
            {
                UserName = userName,
                Email = externalLoginInfo.Principal.FindFirst(ClaimTypes.Email).Value
            };
            Microsoft.AspNetCore.Identity.SignInResult result =
                await _signInManager.ExternalLoginSignInAsync(
                    externalLoginInfo.LoginProvider, externalLoginInfo.ProviderKey, false);
            if (!result.Succeeded)
            {
                var identityResult = await CreateIdentityUserAsync(externalLoginInfo);
                if (!identityResult.Succeeded)
                {
                    return View("Login");
                }
            }

            return View("Login");

        }

        public IActionResult DuendeLogin()
        {
            string redirectUrl = Url.Action("DuendeResponse", "Account");
            string scheme = "oidc";
            //OpenIdConnectDefaults.AuthenticationScheme;
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(scheme, redirectUrl);
            return new ChallengeResult(scheme, properties);
        }

        public async Task<IActionResult> DuendeResponse()
        {
            ExternalLoginInfo externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync();
            if (externalLoginInfo == null)
            {
                return RedirectToAction(nameof(Login));
            }

            //Put info provided by google (claims) into a viewmodel
            string userName = externalLoginInfo.Principal.FindFirst(ClaimTypes.Name).Value;
            //make sure username is unique
            UserViewModel model = new UserViewModel()
            {
                UserName = userName,
                Email = externalLoginInfo.Principal.FindFirst(ClaimTypes.Email).Value
            };
            Microsoft.AspNetCore.Identity.SignInResult result =
                await _signInManager.ExternalLoginSignInAsync(
                    externalLoginInfo.LoginProvider, externalLoginInfo.ProviderKey, false);
            if (!result.Succeeded)
            {
                var identityResult = await CreateIdentityUserAsync(externalLoginInfo);
                if (!identityResult.Succeeded)
                {
                    return View("Login");
                }
            }

            return View("Login"); 
        }
        public async Task<IActionResult> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return View("Login");
        }
    }
}
