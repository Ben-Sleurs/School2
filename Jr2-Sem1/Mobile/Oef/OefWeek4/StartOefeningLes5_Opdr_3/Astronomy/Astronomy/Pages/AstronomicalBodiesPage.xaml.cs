namespace Astronomy.Pages;

public partial class AstronomicalBodiesPage : ContentPage
{
	public AstronomicalBodiesPage()
	{
		InitializeComponent();
        //btnEarth.Clicked += async (s, e) => { await Shell.Current.GoToAsync($"{nameof(AstronomicalBodyPage)}?bodyName=earth"); };
        btnEarth.Clicked += async (s, e) => await Navigation.PushAsync(new AstronomicalBodyPage("earth"));
        btnMoon.Clicked += async (s, e) => { await Shell.Current.GoToAsync($"{nameof(AstronomicalBodyPage)}?bodyName=moon"); };
        btnSun.Clicked += async (s, e) => { await Shell.Current.GoToAsync($"{nameof(AstronomicalBodyPage)}?bodyName=sun"); };
        btnComet.Clicked += async (s, e) => { await Shell.Current.GoToAsync($"{nameof(AstronomicalBodyPage)}?bodyName=comet"); };
    }
}