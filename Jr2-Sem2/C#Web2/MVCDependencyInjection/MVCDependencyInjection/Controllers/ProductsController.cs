using Microsoft.AspNetCore.Mvc;
using MVCDependencyInjection.Models;
using MVCDependencyInjection.Services;

namespace MVCDependencyInjection.Controllers
{
    public class ProductsController : Controller
    {
        IProductRepository _productRepository;
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            return View(_productRepository.Products);
        }
        public IActionResult Create()
        {
            Product p = new Product();
            return View(p);
        }
        [HttpPost]
        public IActionResult Create(Product p)
        {
            _productRepository.Add(p);
            return RedirectToAction("Index", "Products");
        }
    }
}
