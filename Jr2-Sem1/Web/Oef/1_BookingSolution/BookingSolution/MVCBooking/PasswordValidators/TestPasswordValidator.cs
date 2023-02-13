using Microsoft.AspNetCore.Identity;

namespace MVCBooking.PasswordValidator
{
    public class TestPasswordValidator : PasswordValidator<IdentityUser>
    {
        public override Task<IdentityResult> ValidateAsync( UserManager<IdentityUser> manager, IdentityUser user, string password)
        {
            return base.ValidateAsync(manager,user,password);
        }
    }
}
