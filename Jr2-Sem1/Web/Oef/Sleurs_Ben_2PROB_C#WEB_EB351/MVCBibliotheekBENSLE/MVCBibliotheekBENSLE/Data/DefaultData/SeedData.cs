using Microsoft.AspNetCore.Identity;
using MVCBibliotheekBENSLE.Models.Data;
using System.Data;

namespace MVCBibliotheekBENSLE.Data.DefaultData
{
    public static class SeedData
    {
        static AppDbContext? _context;
        static RoleManager<IdentityRole>? _roleManager;
        static UserManager<IdentityUser>? _userManager;
        public static async void EnsurePopulated(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                _userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                await VoegRollenToeAsync();

                await CreateIdentityRecordAsync("BibManager", "bibmanager@pxl.be", "Bib99", Roles.BibManager);

                VoegStartDataToe();
            }
        }

        private static void VoegStartDataToe()
        {
            if (_context == null)
            {
                return;
            }
            if(_context.VergaderZaal!=null && !_context.VergaderZaal.Any())
            {
                VergaderZaal vz = new VergaderZaal() { Naam = "Zaal Frodo", AantalPersonen = 10 };
                _context.VergaderZaal.Add(vz);
                _context.SaveChanges();
            }
        }
        private static async Task VoegRollenToeAsync()
        {
            if (_roleManager != null && !_roleManager.Roles.Any())
            {
                await VoegRolToeAsync(Roles.BibManager);
                await VoegRolToeAsync(Roles.BibGebruiker);


            }
        }

        private static async Task VoegRolToeAsync(string roleName)
        {
            if (_roleManager != null && !await _roleManager.RoleExistsAsync(roleName))
            {
                IdentityRole role = new IdentityRole(roleName);
                await _roleManager.CreateAsync(role);
            }
        }
        private static async Task CreateIdentityRecordAsync(string userName, string email, string pwd, string role)
        {

            if (_userManager != null && await _userManager.FindByEmailAsync(email) == null &&
                    await _userManager.FindByNameAsync(userName) == null)
            {
                var identityUser = new IdentityUser() { Email = email, UserName = userName };
                var result = await _userManager.CreateAsync(identityUser, pwd);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(identityUser, role);
                }
            }
        }
    }
}
