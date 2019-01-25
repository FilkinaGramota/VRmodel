using System;
using VideoRental.VRmodel;
using System.Linq;


namespace VideoRental.Repositories
{
    class GenreRepClass:ClassRepEntity<Genre>, GenreRep
    {
        public MineVideoRental MineVideoRentalContext 
        {
            get { return dbContext as MineVideoRental; }
        }
        public GenreRepClass(MineVideoRental mineVideoRental) : base(mineVideoRental) 
        {

        }
        public IQueryable<Genre> GetGenresOfFilm(Film film)
        {
            return MineVideoRentalContext.Genres.Where(genre => genre.Films.Any(f => f.Id == film.Id));
        }

        public bool IsGenreExists(Genre genre)
        {
            return MineVideoRentalContext.Genres.Select(g => g.Type).Contains(genre.Type);
        }

        public Genre GetSame(Genre genre)
        {
            return MineVideoRentalContext.Genres.SingleOrDefault(g => g.Type == genre.Type);
        }
    }
}
