namespace MVCEndpointRouting.CustomConstraint;

public class FirstNameConstraint : IRouteConstraint
{
    private string[] firstNames = new[] { "Kristof,JoepJoep" };
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values,
        RouteDirection routeDirection)
    {
        return firstNames.Contains(values[routeKey]);
    }
}