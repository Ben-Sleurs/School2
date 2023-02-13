namespace OefWeek3;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private async void BtnOef2(object sender, EventArgs e)
	{
        await Shell.Current.GoToAsync(nameof(Oefening2));
    }
}

