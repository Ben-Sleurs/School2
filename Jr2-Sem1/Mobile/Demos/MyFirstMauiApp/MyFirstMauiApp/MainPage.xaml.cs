namespace MyFirstMauiApp;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void TranslateBtn_Clicked(object sender, EventArgs e)
	{
		string input = InputEntry.Text;
        double sliderOutput = MySlider.Value;
        string output = LeetTranslator.ToLeet(input, sliderOutput*100);
		OutputLbl.Text = output;
	}
}

		