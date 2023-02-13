namespace ViewsDemo;

public partial class CollectionViewDemo : ContentPage
{
	public CollectionViewDemo()
	{
		InitializeComponent();
		List<MiniFigure> myHeroes = new List<MiniFigure>();
		//myHeroes.Add(new MiniFigure() {Name="Sam",ImageUrl="Sam.jpg", Race="Hobbit"});
  //      myHeroes.Add(new MiniFigure() { Name = "Elrond", ImageUrl = "elrond.jpg", Race = "Elf" });
  //      myHeroes.Add(new MiniFigure() { Name = "Gollum", ImageUrl = "gollum.jpg", Race = "RiverPeople" });

        Examplecoll.ItemsSource = myHeroes;
		
	}
}