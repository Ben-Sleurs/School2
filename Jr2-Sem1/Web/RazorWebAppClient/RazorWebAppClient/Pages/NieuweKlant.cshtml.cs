using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorWebAppClient.Data;

namespace RazorWebAppClient.Pages
{
    public class NieuweKlantModel : PageModel
    {
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            string naam = Request.Form["Klant"];
            int locatieId = int.Parse(Request.Form["SelectLocatie"].FirstOrDefault());
            Databank.AddKlant(naam,locatieId);
            return RedirectToPage("Klanten");
        }
    }
}
