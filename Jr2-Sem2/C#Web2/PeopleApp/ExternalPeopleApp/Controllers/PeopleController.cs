using ExternalPeopleApp.Models;
using ExternalPeopleApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExternalPeopleApp.Controllers;

public class PeopleController : Controller
{
    private readonly IPeopleRepository _peopleRepo;
    private readonly IDepartmentRepository _departmentRepo;
    private readonly ILocationRepository _locationRepo;

    public PeopleController(IPeopleRepository peopleRepo, IDepartmentRepository departmentRepo,
        ILocationRepository locationRepo)
    {
        _peopleRepo = peopleRepo;
        _departmentRepo = departmentRepo;
        _locationRepo = locationRepo;
    }

    public IActionResult Index()
    {
        return View(_peopleRepo.GetAll());
    }

    public IActionResult Create()
    {
        var model = new PersonEditViewModel
        {
            DepartmentSelectListItems = _departmentRepo.GetAll().Select(d =>
                new SelectListItem
                {
                    Text = d.Name,
                    Value = d.Id.ToString()
                }).ToList(),
            LocationSelectListItems = _locationRepo.GetAll().Select(l =>
                new SelectListItem
                {
                    Text = l.City,
                    Value = l.Id.ToString()
                }).ToList()
        };
        return View(model);
    }
    [HttpPost]
    public IActionResult Create(PersonEditModel model)
    {
        _peopleRepo.Add(model);
        return RedirectToAction(nameof(Index));
    }
}