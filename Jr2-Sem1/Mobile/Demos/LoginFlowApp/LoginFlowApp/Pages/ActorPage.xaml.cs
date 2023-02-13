using LoginFlowApp.Models;

namespace LoginFlowApp.Pages;

public partial class ActorPage : ContentPage
{
    Actor actor1 = new Actor()
    {
        Name = "Merlo",
        FirstName = "Davy",
        BirthYear = 1988,
        ProfilePictureUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS9em1usmHX7UeUEZxJFB_5vq-n3LrikNr7fQ&usqp=CAU"
    };
    public ActorPage()
	{
		InitializeComponent();
		
		BindingContext = actor1;
	}

	private void Button_Clicked(object sender, EventArgs e)
	{
		actor1.Name = "Connery";
		actor1.FirstName = "Sean";
	}
}