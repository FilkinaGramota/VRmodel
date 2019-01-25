using System;
using VideoRental.Repositories;

namespace VideoRental
{
    interface UnitOfWork:IDisposable
    {
        CasseteRep CassetteRepasitory { get; set; }
   
        ClientRep ClientRepasitory { get; set; }
       
        FilmRep FilmRepasitory { get; set; }

        GenreRep GenreRepasitory { get; set; }

        OrderRep OrderRepasitory { get; set; }

        int Save(); //work with repository
    }
}
