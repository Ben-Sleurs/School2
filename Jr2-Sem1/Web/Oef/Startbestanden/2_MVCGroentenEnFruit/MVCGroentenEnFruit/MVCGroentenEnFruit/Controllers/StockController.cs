using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCGroentenEnFruit.Data;
using MVCGroentenEnFruit.Models.Data;
using MVCGroentenEnFruit.Models.ViewModels;

namespace MVCGroentenEnFruit.Controllers
{
    public class StockController : Controller
    {
        AppDbContext _context;
        UserManager<IdentityUser> _userManager;
        SignInManager<IdentityUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;
        public StockController(AppDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public ActionResult IndexAsync()
        {
            var aankoopOrders = _context.AankoopOrders;
            var verkoopOrders = _context.VerkoopOrders;
            var stock = _context.Artikels.Select(x => new StockViewModel { ArtikelId = x.ArtikelId, ArtikelNaam = x.Naam, Hoeveelheid = 0 });
            foreach (var item in stock)
            {
                foreach (var aankoopOrder in aankoopOrders)
                {
                    if (aankoopOrder.ArtikelId == item.ArtikelId)
                    {
                        item.Hoeveelheid += aankoopOrder.Hoeveelheid;
                    }
                }
                foreach (var verkoopOrder in verkoopOrders)
                {
                    if (verkoopOrder.ArtikelId == item.ArtikelId)
                    {
                        item.Hoeveelheid -= verkoopOrder.Hoeveelheid;
                    }
                }
            }
            return View(stock);
        }
    }
}
