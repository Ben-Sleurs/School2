using Microsoft.AspNetCore.Mvc;
using MVCPxlOrderSystem.Data;

namespace MVCPxlOrderSystem.Components
{
    public class RegionsViewComponent: ViewComponent
    {
        private PxlOrderSystemContext _context;
        public RegionsViewComponent(PxlOrderSystemContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var regions = _context.Regions;
            return View(regions);
        }
    }
}
