using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExternalPeopleApp.Models
{
    public class PersonEditViewModel : PersonEditModel​
    {
        public List<SelectListItem>? DepartmentSelectListItems { get; set; }
        public List<SelectListItem>? LocationSelectListItems { get; set; }
    }
}
