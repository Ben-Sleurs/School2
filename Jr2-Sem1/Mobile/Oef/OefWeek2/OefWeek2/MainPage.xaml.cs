namespace OefWeek2;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
    }
	public async void test()
	{
        await DisplayAlert("Alert", "You have been alerted", "OK");
    }

	private async void testClick(object sender, EventArgs e)
	{
        bool answer = await DisplayAlert("Question?", "Would you like to play a game", "Sisi Hombre", "Nono Hombre");
		DisplayAlert("Alert", $"Your answer was {answer}", "Sure thing buddy");
    }
}

