namespace PXLFilmzaal.Models.Data
{
    public class Acteur
    {
        public int ActeurId { get; set; }
        public string ActeurNaam { get; set; }
        public int FilmImageId { get; set; }
        public FilmImage FilmImage { get; set; }
    }
}
