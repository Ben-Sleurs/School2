using Microsoft.AspNetCore.Mvc;
using MVCEndpointRouting.Models.ViewModels;
using MVCEndpointRouting.Services;

namespace MVCEndpointRouting.Controllers;

public class RoutingController : Controller
{
    private IRoutingRepository _routingRepo;

    public RoutingController(IRoutingRepository routingRepo)
    {
        _routingRepo = routingRepo;
    }
    // GET
    public IActionResult RoutingInt(string id)
    {
        RoutingModel routingModel = new RoutingModel("Routing");
        routingModel.ActionName = "RoutingInt";
        routingModel.RoutingParameters.Add("id",id);
        _routingRepo.Add(routingModel);
        ViewBag.ValueofId = id ?? "Null Value";

        return View();
    }

    public IActionResult RoutingIdAndName(string id, string name)
    {
        RoutingModel routingModel = new RoutingModel("Routing");
        routingModel.ActionName = "RoutingIntAndName";
        routingModel.RoutingParameters.Add("id",id);
        routingModel.RoutingParameters.Add("name",name);
        ViewBag.ValueofId = id ?? "Null Value";
        ViewBag.ValueofName = name ?? "Null Value";

        _routingRepo.Add(routingModel);
        return View("RoutingInt");
    }
}