using VideoRental.VRmodel;

namespace VideoRental.Repositories
{
    interface ClientRep:EntityRep<Client>
    {
        bool IsClientExists(Client client);
        Client GetSame(Client client);
    }
}
