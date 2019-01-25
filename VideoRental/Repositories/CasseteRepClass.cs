using System;
using System.Linq;
using VideoRental.VRmodel;

namespace VideoRental.Repositories
{
    class CasseteRepClass : ClassRepEntity<Cassette>, CasseteRep

    {
        public MineVideoRental MineVideoRentalContext //property
        {
            get { return dbContext as MineVideoRental; }
        }
        public CasseteRepClass(MineVideoRental mineVideoRental) : base(mineVideoRental) //constructor context bd mine video rental and call base costructor
        {

        }
        public IQueryable<Cassette> GetCassetteMin(int Amount)
        {
            return MineVideoRentalContext.Cassettes.Where(cassette => cassette.Amount < Amount);
        }

        public IQueryable<Cassette> GetCassetteMax(int Amount)
        {
            return MineVideoRentalContext.Cassettes.Where(cassette => cassette.Amount > Amount);
        }

        public bool IsCassetteExists(Cassette cassette)
        {
            return MineVideoRentalContext.Cassettes.Select(c => c.Title).Contains(cassette.Title);
        }

        public Cassette GetSame(Cassette cassette)
        {
            return MineVideoRentalContext.Cassettes.SingleOrDefault(c => c.Title == cassette.Title);
        }

        public override void Delete(Cassette cassette)
        {
            var orders = MineVideoRentalContext.Orders.Where(o => o.Cassettes.Any(c => c.Id == cassette.Id));
            foreach (var order in orders)
            {
                var clients = MineVideoRentalContext.Clients.Where(cl => cl.Orders.Any(o => o.Id == order.Id));
                MineVideoRentalContext.Clients.RemoveRange(clients.ToList());
            }

            MineVideoRentalContext.Orders.RemoveRange(orders);
            MineVideoRentalContext.Cassettes.Remove(cassette);
        }
    }
}

