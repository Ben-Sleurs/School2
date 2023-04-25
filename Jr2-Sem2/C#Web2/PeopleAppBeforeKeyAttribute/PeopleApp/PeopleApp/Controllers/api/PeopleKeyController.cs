using Microsoft.AspNetCore.Mvc;
using PeopleApp.Attributes;
using PeopleApp.Models;
using PeopleApp.Services;
using PeopleApp.ViewModels;

namespace PeopleApp.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class PeopleKeyController : Controller
    {
        private readonly IPersonRepository _personRepo;
        public PeopleKeyController(IPersonRepository personRepo)
        {
            _personRepo = personRepo;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Person> people = _personRepo.GetAll();
            List<PersonOutputModel> models = people.Select(p => PersonOutputModel.FromPerson(p)).ToList();
            return Ok(models);
        }
    }
}
