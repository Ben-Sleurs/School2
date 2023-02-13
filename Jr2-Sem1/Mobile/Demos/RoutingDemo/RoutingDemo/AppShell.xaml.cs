namespace RoutingDemo;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(RestaurantPage), typeof(RestaurantPage));
        Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
    }
}
