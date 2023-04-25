using MVCEndpointRouting.Models.ViewModels;

namespace MVCEndpointRouting.Services;

public interface IRoutingRepository
{
    IEnumerable<RoutingModel> RoutingModels { get; }
    void Add(RoutingModel routingModel);
}