using System.ComponentModel.DataAnnotations;

namespace PXLFilmzaal.Models.Data
{
    public class FilmImage
    {
        public FilmImage()
        {
            FilmImageId = Guid.NewGuid().ToString();
        }
        public string FilmImageId { get; set; }
        public byte[]? FilmImageData { get; set; }
        [Required]
        public string FilmImageName { get; set; }
        public ICollection<Film>? Films { get; set; }
    }
}
