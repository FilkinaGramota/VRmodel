using VideoRental.VRmodel;
using VideoRental.Repositories;

namespace VideoRental
{ 
    class ClassUnitOfWorkRep: UnitOfWork
    {
        private readonly MineVideoRental context;

        public CasseteRep CassetteRepasitory { get; set; }
        public OrderRep OrderRepasitory { get; set; }
        public FilmRep FilmRepasitory { get; set; }
        public GenreRep GenreRepasitory { get; set; }
        public ClientRep ClientRepasitory { get; set; }

        public ClassUnitOfWorkRep(MineVideoRental context)
        {
            this.context = context;
            CassetteRepasitory = new CasseteRepClass(context);
            OrderRepasitory = new OrderRepClass(context);
            FilmRepasitory = new FilmRepClass(context);
            GenreRepasitory = new GenreRepClass(context);
            ClientRepasitory = new ClientRepClass(context);
        }

        public int Save()
        {
           return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
