using Microsoft.AspNetCore.Identity;
using MVCBibliotheekBENSLE.Models.Data;

namespace MVCBibliotheekBENSLE.PasswordValidators
{
    public class CapitalPasswordValidator : PasswordValidator<Gebruiker>
    {
        public override async Task<IdentityResult> ValidateAsync(UserManager<Gebruiker> manager, Gebruiker user, string password)
        {
            List<IdentityError> errors = new List<IdentityError>();
            IdentityResult result = await base.ValidateAsync(manager, user, password);
            var validation = result.Succeeded;
            if (password.ToLower().Equals(password))
            {
                errors.Add(new IdentityError { Description = "Paswoord moet een hoofdletter bevatten!" });
                validation = false;
            }
            return validation ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
        }

    }
}
