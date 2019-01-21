using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoRental.VRmodel;
using VideoRental.Repositories;
using System.Threading.Tasks;

namespace VideoRental
{ //repository of repository хранит ссылки на раб репозитории argo является единицей работы
    class ClassUnitOfWorkRep: UnitOfWork
    {
        private static ClassUnitOfWorkRep instance;
        private static readonly object locker = new Object();

        private readonly MineVideoRental context;

        public CasseteRep CassetteRepasitory { get; set; }
        public OrderRep OrderRepasitory { get; set; }
        public FilmRep FilmRepasitory { get; set; }
        public GenreRep GenreRepasitory { get; set; }
        public ClientRep ClientRepasitory { get; set; }

        private ClassUnitOfWorkRep(MineVideoRental context)
        {
            this.context = context;
            CassetteRepasitory = new CasseteRepClass(context);
            OrderRepasitory = new OrderRepClass(context);
            FilmRepasitory = new FilmRepClass(context);
            GenreRepasitory = new GenreRepClass(context);
            ClientRepasitory = new ClientRepClass(context);
        }
        // singleton
        public static ClassUnitOfWorkRep GetUnitOfWork()
        {
            if (instance == null)
            {
                lock (locker)
                {
                    if (instance == null)
                        instance =  new ClassUnitOfWorkRep(new MineVideoRental());
                }
            }
            return instance;
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
