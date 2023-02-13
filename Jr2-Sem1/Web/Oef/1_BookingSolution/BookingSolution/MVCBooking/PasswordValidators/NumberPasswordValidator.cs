using ClassLibBooking.DataModels;
using Microsoft.AspNetCore.Identity;

namespace MVCBooking.PasswordValidators
{
    public class NumberPasswordValidator: PasswordValidator<Student>
    {
        public override async Task<IdentityResult> ValidateAsync(UserManager<Student> manager, Student user, string password)
        {
            List<IdentityError> errors = new List<IdentityError>();
            IdentityResult result = await base.ValidateAsync(manager, user, password);
            var validation = result.Succeeded;
            if (password.Contains("123"))
            {
                errors.Add(new IdentityError { Description = "Paswoord mag geen 123 bevatten!" });
                validation = false;
            }
            return validation ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
        }
    }
}
