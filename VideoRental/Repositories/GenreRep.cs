using System.Linq;
using VideoRental.VRmodel;

namespace VideoRental.Repositories
{
    interface GenreRep:EntityRep<Genre>
    {
        IQueryable<Genre> GetGenresOfFilm(Film film);
        bool IsGenreExists(Genre genre);
        Genre GetSame(Genre genre);
    }
}
