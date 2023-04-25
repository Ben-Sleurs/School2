using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestTokenAuthentication.Data;
using RestTokenAuthentication.Models;

namespace RestTokenAuthentication.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegistrationModel model)
        {
            var searchUser = await _userManager.FindByNameAsync(model.UserName);
            if (searchUser != null) return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                Status = "Error",
                Message = "User already exists!"
            });
            var user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Status = "Error",
                    Message = "User creation failed!"
                });
            return Ok(new
            {
                Status = "Success",
                Message = "User created successfully!"
            });
        }
    }
    
}
