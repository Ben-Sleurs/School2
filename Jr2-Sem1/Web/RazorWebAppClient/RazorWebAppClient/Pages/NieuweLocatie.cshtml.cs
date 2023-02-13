using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorWebAppClient.Data;

namespace RazorWebAppClient.Pages
{
    public class NieuweLocatieModel : PageModel
    {
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            string postCode = Request.Form["PostCode"];
            string city = Request.Form["City"];
            Databank.AddLocation(postCode, city);
            return RedirectToPage("Locaties");
        }
    }
}
