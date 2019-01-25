using VideoRental.VRmodel;


namespace VideoRental.Repositories
{
    interface OrderRep:EntityRep<Order>
    {
        void UpdateStartDate(Order order);
        void UpdateEndDate(Order order);
        void UpdateFactEndDate(Order order);
        void UpdateCassetteList(Order order);
    }
}
