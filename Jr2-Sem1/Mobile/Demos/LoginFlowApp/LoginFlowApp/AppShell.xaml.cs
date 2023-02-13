using System.Windows.Input;

namespace LoginFlowApp;

public partial class AppShell : Shell
{
	public ICommand LogoutCommand => new Command(async () =>  GoToAsync("//LoginPage"));
	public AppShell()
	{
		InitializeComponent();
		BindingContext = this;
	}
}
