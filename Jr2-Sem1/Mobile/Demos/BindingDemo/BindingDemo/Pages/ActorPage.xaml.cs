using BindingDemo.Models;

namespace BindingDemo.Pages;

public partial class ActorPage : ContentPage
{
	private Actor actor1;
	public ActorPage()
	{
		InitializeComponent();
		actor1 = new Actor()
		{
			Name = "Boger",
			FirstName = "Bobin",
			BirthYear = 2007,
			ProfilePictureUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS9em1usmHX7UeUEZxJFB_5vq-n3LrikNr7fQ&usqp=CAU"
		};
		BindingContext = actor1;
	}

	private void Button_Clicked(object sender, EventArgs e)
	{
		actor1.Name = "Michel";
		actor1.FirstName = "Charles";
	}
}