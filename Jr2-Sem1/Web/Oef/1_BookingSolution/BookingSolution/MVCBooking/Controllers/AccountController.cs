using ClassLibBooking.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCBooking.Models;

namespace MVCBooking.Controllers
{
    public class AccountController : Controller
    {
        UserManager<Student> _userManager;
        SignInManager<Student> _signInManager;
        public AccountController(UserManager<Student> userManager, SignInManager<Student> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        //[HttpPost] 
        //public IActionResult Register(RegisterViewModel registerViewModel) 
        //{ 
        //    if (ModelState.IsValid)
        //        if (CreateUser(registerViewModel) == IdentityResult.Success)
        //            return View("Login"); 
        //    return View(); 
        //}
        private IdentityResult CreateUser(RegisterViewModel registerViewModel) 
        { 
            return IdentityResult.Success; 
        }
        private void Errors(IdentityResult identityResult) 
        { 
            foreach (var error in identityResult.Errors) 
            { 
                ModelState.AddModelError("", error.Description); 
            }
        }
        private async Task<IdentityResult> CreateUserAsync(RegisterViewModel registerViewModel)
        {
            var student = new Student();
            student.FirstName = registerViewModel.FirstName;
            student.LastName = registerViewModel.LastName;
            student.Email = registerViewModel.Email;
            student.UserName = registerViewModel.UserName;
            return await _userManager.CreateAsync(student, registerViewModel.Password);
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
            var student = await _userManager.FindByEmailAsync(login.Email);
            if (student is not null)
            {
                var result = await _signInManager.PasswordSignInAsync(student.UserName, login.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "Home");
                }
            }
            return View();
        }
        #endregion
    }
}   
