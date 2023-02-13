namespace RoutingDemo;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private async void RestaurantBtn(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(RestaurantPage));
	}
    private async void AboutBtn(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AboutPage));
    }
}

