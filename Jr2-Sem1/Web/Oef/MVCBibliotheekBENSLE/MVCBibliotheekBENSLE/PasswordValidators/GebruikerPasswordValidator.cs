using Microsoft.AspNetCore.Identity;
using MVCBibliotheekBENSLE.Models.Data;

namespace MVCBibliotheekBENSLE.PasswordValidators
{
    public class GebruikerPasswordValidator : PasswordValidator<Gebruiker>
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
            if (password.ToUpper().Equals(password))
            {
                errors.Add(new IdentityError { Description = "Paswoord moet een kleine letter bevatten!" });
                validation = false;
            }
            if (password.Length < 5)
            {
                errors.Add(new IdentityError { Description = "paswoord moet minstens 5 karakters lang zijn" });
                validation = false;

            }
            string numbers = "0123456789";
            if (!password.Any(x => numbers.Contains(x)))
            {
                errors.Add(new IdentityError { Description = "paswoord moet minstens 1 nummer bevatten" });
                validation = false;
            }

            return validation ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
        }

    }
}
