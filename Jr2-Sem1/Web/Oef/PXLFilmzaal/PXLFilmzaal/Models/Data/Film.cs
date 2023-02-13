using System.ComponentModel.DataAnnotations;

namespace PXLFilmzaal.Models.Data
{
    public class Film
    {
        public Film()
        {
            FilmId = Guid.NewGuid().ToString();
        }
        public string FilmId { get; set; }
        [Required]
        public string FilmNaam { get; set; }
        public int? FilmImageId { get; set; }
        public FilmImage? FilmImage { get; set; }
    }
}
