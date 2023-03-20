using MVCEndpointRouting.Models.ViewModels;

namespace MVCEndpointRouting.Services;

public class RoutingRepository : IRoutingRepository
{
    private List<RoutingModel> _routingModels = new List<RoutingModel>();
    public IEnumerable<RoutingModel> RoutingModels => _routingModels;
    public void Add(RoutingModel routingModel)
    {
        _routingModels.Add(routingModel);
    }
}