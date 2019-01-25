using VideoRental.VRmodel;
using System.Linq;

namespace VideoRental.Repositories
{
    interface FilmRep:EntityRep<Film>
    {
        // могут быть разные фильмы с одинаковыми названиями
        IQueryable<Film> GetFilmsByTitle(string title);
        bool IsFilmExists(Film film);
        Film GetSame(Film film);
    }
}
