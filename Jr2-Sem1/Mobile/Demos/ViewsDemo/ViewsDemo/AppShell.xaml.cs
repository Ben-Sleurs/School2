namespace ViewsDemo;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(CollectionViewDemo), typeof(CollectionViewDemo));
    }
}
