using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebAppClient.Pages
{
    public class SelecteerLocatieModel : PageModel
    {
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            var id = Request.Form["SelectLocatie"].FirstOrDefault();
            return RedirectToPage("LocatieDetails", new {id=int.Parse(id)});
        }
    }
}
