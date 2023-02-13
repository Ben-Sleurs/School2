namespace OefWeek3;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(Oefening2), typeof(Oefening2));
    }
}
