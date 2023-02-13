using HogeschoolPXL.Data;
using Microsoft.AspNetCore.Mvc;

namespace HogeschoolPXL.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private AppDbContext _context;
        public NavigationMenuViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["nav"];
            var navigationButtons = new List<string>() { "Gebruiker", "Lector", "Student", "Inschrijving", "Handboek", "Vak", "VakLector", "AcademieJaar" };
            return View(navigationButtons);
        }
    }
}
