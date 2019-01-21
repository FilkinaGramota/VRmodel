using System;
using VideoRental.VRmodel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoRental.Repositories
{
    class ClientRepClass:ClassRepEntity<Client>,ClientRep

    {
        public MineVideoRental MineVideoRentalContext //property
        {
            get { return dbContext as MineVideoRental; }
        }
        public ClientRepClass(MineVideoRental mineVideoRental) : base(mineVideoRental) //constructor context bd mine video rental and call base costructor
        {

        }

        public bool IsClientExists(Client client)
        {
            bool first = MineVideoRentalContext.Clients.Select(c => c.Name.FirstName).Contains(client.Name.FirstName);
            bool last = MineVideoRentalContext.Clients.Select(c => c.Name.LastName).Contains(client.Name.LastName);
            return (first && last);
        }

        public Client GetSame(Client client)
        {
            return MineVideoRentalContext.Clients.SingleOrDefault(c => c.Name.Equals(client.Name));
        }
    }
}
