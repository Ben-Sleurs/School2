using PXLFilmzaal.Helpers;
using PXLFilmzaal.Models.Data;

namespace PXLFilmzaal.Data
{
    public static class SeedData
    {
        public static void EnsurePopulated(WebApplication app)
        {
            using(var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                if (!context.FilmImages.Any())
                {
                    string fileName = @"C:\School\Jr2-Sem1\Web\Stuff\Charles.jpg";
                    var charlesImage = new FilmImage();
                    charlesImage.FilmImageName = "Charles Michel Toppest of G's";
                    charlesImage.FilmImageData = File.ReadAllBytes(fileName);
                    //charlesImage.FilmImageData = FileHelper.CreateByteArrayFromFile(fileName);
                    context.FilmImages.Add(charlesImage);

                    var charlesFilm = new Film();
                    charlesFilm.FilmNaam = "Charles Michel: the story of the toppest of G's";
                    charlesFilm.FilmImage = charlesImage;
                    context.Films.Add(charlesFilm);
                    context.SaveChanges();
                }
            }
        }
    }
}
