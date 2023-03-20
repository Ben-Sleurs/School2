namespace MVCEndpointRouting.Models.ViewModels;

public class RoutingModel
{
    public RoutingModel(string controller)
    {
        Controller = controller;
        Date = DateTime.Now;
    }

    public string Controller { get; set; }
    public string ActionName { get; set; }
    public DateTime Date { get; set; }

    public Dictionary<string, string> RoutingParameters =
        new Dictionary<string, string>();

    public string Parameters => string.Join(',', RoutingParameters.Select((x =>
        $"{x.Key}:{x.Value}")));
}