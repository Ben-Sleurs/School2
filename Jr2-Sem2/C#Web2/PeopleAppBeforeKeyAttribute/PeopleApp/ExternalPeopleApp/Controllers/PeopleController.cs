using ExternalPeopleApp.Models;
using ExternalPeopleApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExternalPeopleApp.Controllers
{
    public class PeopleController : Controller
    {
        IDepartmentApiRepository _depRepo;
        ILocationApiRepository _locRepo;
        IPeopleApiRepository _personApi;
        public PeopleController(IDepartmentApiRepository depRepo, ILocationApiRepository locRepo, IPeopleApiRepository personApi)
        {
            _depRepo = depRepo;
            _locRepo = locRepo;
            _personApi = personApi;
        }
        public IActionResult Index()
        {
            return View(_personApi.GetAll());
        }
        private PersonEditViewModel GetPersonEditViewModel()
        {
            var model = new PersonEditViewModel();
            model.DepartmentSelectListItems = _depRepo.GetAll().Select(
                d => new SelectListItem { Text = d.Name, Value = d.Id.ToString() }).ToList();

            model.LocationSelectListItems = _locRepo.GetAll().Select(
                l => new SelectListItem { Text = l.City, Value = l.Id.ToString() }).ToList();
            return model;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(GetPersonEditViewModel());
        }
        [HttpPost]
        public IActionResult Create(PersonEditViewModel model)
        {
            _personApi.Add(model);
            return RedirectToAction("Index");
        }
    }
}
