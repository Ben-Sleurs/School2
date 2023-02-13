namespace ViewsDemo;

public partial class MainPage : ContentPage
{
	int count = 0;
    string googleQuery = "https://www.google.com/search?q=";

    public MainPage()
	{
		InitializeComponent();
	}

	private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
	{
		string search = ((SearchBar)sender).Text;
		search = search.Replace(" ", "+");
		WebView.Source = googleQuery+search;
	}

	private void Switch_Toggled(object sender, ToggledEventArgs e)
	{
		
	}

	private async void BtnCollectionView(object sender, EventArgs e)
	{
        await Shell.Current.GoToAsync(nameof(CollectionViewDemo));
    }
}

