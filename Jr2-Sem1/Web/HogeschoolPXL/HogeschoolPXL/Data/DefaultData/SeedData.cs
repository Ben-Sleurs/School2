using HogeschoolPXL.Models.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HogeschoolPXL.Data.DefaultData
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

                await CreateIdentityRecordAsync(Roles.Student, "student@pxl.be", "Student123!", Roles.Student);
                await CreateIdentityRecordAsync("Ben.Sleurs@student.pxl.be", "Ben.Sleurs@student.pxl.be", "Student123!", Roles.Student);
                await CreateIdentityRecordAsync(Roles.Admin, "admin@pxl.be", "Admin456!", Roles.Admin);
                await CreateIdentityRecordAsync(Roles.Lector, "lector@pxl.be", "Lector789!", Roles.Lector);
                await CreateIdentityRecordAsync("Kristof.Palmaers@pxl.be", "Kristof.Palmaers@pxl.be", "Lector789!", Roles.Lector);


                VoegStartDataToe();
            }
        }
        private static void VoegStartDataToe()
        {
            if (_context != null)
            {
                if (_context.Gebruiker !=null && !_context.Gebruiker.Any())
                {
                    Gebruiker admin = new Gebruiker() { Naam = "Admin", Voornaam = "Admin", Email = "admin@pxl.be", Role = Roles.Admin };
                    _context.Gebruiker.Add(admin);
                    _context.SaveChanges();
                }
                if (_context.Student != null && !_context.Student.Any())
                {
                    Gebruiker g = new Gebruiker() { Naam = "Sleurs", Voornaam = "Ben", Email = "Ben.Sleurs@student.pxl.be", Role=Roles.Student };
                    _context.Gebruiker.Add(g);
                    _context.SaveChanges();
                    Student Ben = new Student() { GebruikerId = _context.Gebruiker.Where(x => x.Email== "Ben.Sleurs@student.pxl.be").FirstOrDefault().GebruikerId };
                    _context.Student.Add(Ben);

                    Gebruiker gg = new Gebruiker() { Naam = "Student", Voornaam = "Student", Email = "student@pxl.be", Role = Roles.Student };
                    _context.Gebruiker.Add(gg);
                    _context.SaveChanges();
                    Student student = new Student() { GebruikerId = _context.Gebruiker.Where(x => x.Email == "student@pxl.be").FirstOrDefault().GebruikerId };
                    _context.Student.Add(student);
                    _context.SaveChanges();

                    

                }
                if (_context.Lector != null && !_context.Lector.Any())
                {
                    Gebruiker g = new Gebruiker() { Naam = "Palmaers", Voornaam = "Kristof", Email = "Kristof.Palmaers@pxl.be", Role=Roles.Lector };
                    _context.Gebruiker.Add(g);
                    _context.SaveChanges();
                    Lector Kristof = new Lector() { GebruikerId = _context.Gebruiker.Where(x => x.Email == "Kristof.Palmaers@pxl.be").FirstOrDefault().GebruikerId };
                    _context.Lector.Add(Kristof);
                    _context.SaveChanges();

                    Gebruiker gg = new Gebruiker() { Naam = "Lector", Voornaam = "Lector", Email = "lector@pxl.be", Role = Roles.Lector };
                    _context.Gebruiker.Add(gg);
                    _context.SaveChanges();
                    Lector lector = new Lector() { GebruikerId = _context.Gebruiker.Where(x => x.Email == "lector@pxl.be").FirstOrDefault().GebruikerId };
                    _context.Lector.Add(lector);
                    _context.SaveChanges();
                }
                if (_context.Handboek != null && !_context.Handboek.Any())
                {
                    Handboek h = new Handboek() { Titel = "C#Web1",
                                                  Afbeelding = "data:image/jpeg;base64,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"
                    };
                    _context.Handboek.Add(h);
                    _context.SaveChanges();
                }
                if (_context.Vak != null && !_context.Vak.Any())
                {
                    Vak v = new Vak() { VakNaam = "C#Web1",
                                        HandboekId= _context.Handboek.Where(x => x.Titel =="C#Web1").FirstOrDefault().HandboekId,
                                        Studiepunten=6};
                    _context.Vak.Add(v);
                    _context.SaveChanges();
                }
                if (_context.VakLector != null && !_context.VakLector.Any())
                {
                    VakLector vl = new VakLector() { LectorId = _context.Lector
                                                                .Include(x => x.Gebruiker)
                                                                .Where(x => x.Gebruiker.Email == "Kristof.Palmaers@pxl.be").FirstOrDefault().LectorId,
                                                     VakId = _context.Vak.Where(x => x.VakNaam == "C#Web1").FirstOrDefault().VakId };
                    _context.VakLector.Add(vl);
                    _context.SaveChanges();
                }
                if (_context.AcademieJaar != null && !_context.AcademieJaar.Any())
                {
                    AcademieJaar aj = new AcademieJaar() { StartDatum = new DateTime(2022,9,20) };
                    _context.AcademieJaar.Add(aj);
                    _context.SaveChanges();
                }
                if (_context.Inschrijving != null && !_context.Inschrijving.Any())
                {
                    Inschrijving i = new Inschrijving() {
                        StudentId = _context.Student.Include(x => x.Gebruiker).Where(x => x.Gebruiker.Email == "Ben.Sleurs@student.pxl.be").FirstOrDefault().StudentId,
                        VakLectorId = _context.VakLector.Include(x => x.Vak).Include(x => x.Lector).ThenInclude(x => x.Gebruiker)
                        .Where(x => x.Vak.VakNaam=="C#Web1" && x.Lector.Gebruiker.Email== "Kristof.Palmaers@pxl.be")
                        .FirstOrDefault().VakLectorId,
                        AcademieJaarId = _context.AcademieJaar.Where(x => x.StartDatum == new DateTime(2022,9,20)).FirstOrDefault().AcademieJaarId };
                    _context.Inschrijving.Add(i);
                    _context.SaveChanges();
                }
                _context.SaveChanges();
            }


        }
        private static async Task VoegRollenToeAsync()
        {
            if (_roleManager != null && !_roleManager.Roles.Any())
            {
                await VoegRolToeAsync(Roles.Admin);
                await VoegRolToeAsync(Roles.Student);
                await VoegRolToeAsync(Roles.Lector);
                await VoegRolToeAsync(Roles.TempAdmin);
                await VoegRolToeAsync(Roles.TempStudent);
                await VoegRolToeAsync(Roles.TempLector);


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
