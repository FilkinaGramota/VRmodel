using System;
using VideoRental.VRmodel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoRental.Repositories
{
    interface ClientRep:EntityRep<Client>
    {
        bool IsClientExists(Client client);
        Client GetSame(Client client);
    }
}
