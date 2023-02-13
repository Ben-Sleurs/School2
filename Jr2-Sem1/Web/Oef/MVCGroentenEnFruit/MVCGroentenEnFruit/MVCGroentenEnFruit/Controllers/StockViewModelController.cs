using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCGroentenEnFruit.Data;
using MVCGroentenEnFruit.Models.ViewModels;

namespace MVCGroentenEnFruit.Controllers
{
    public class StockViewModelController : Controller
    {
        private readonly AppDbContext _context;

        public StockViewModelController(AppDbContext context)
        {
            _context = context;
        }

        // GET: StockViewModel
        public async Task<IActionResult> Index()
        {
          var lst = _context.Artikels.Select( x => new StockViewModel()
          //FOUT
          { ArtikelId= x.ArtikelId,
              ArtikelNaam = x.Naam,
              Id = x.ArtikelId,
              Hoeveelheid = x.AankoopOrders.Count - x.VerkoopOrders.Count
          });
              return View( lst);
        }      
    }
}
