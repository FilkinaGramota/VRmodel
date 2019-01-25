using System.Linq;

namespace VideoRental.Repositories
{
    // Базовй интерфейс
    interface EntityRep <Class> where Class: class

    {
        void Add(Class entityclass);
        Class Get(int ID);
        IQueryable<Class> GetAll();
        void Delete(Class entityclass);

    }
}
