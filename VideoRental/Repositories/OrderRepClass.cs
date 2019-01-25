using System;
using VideoRental.VRmodel;

namespace VideoRental.Repositories
{
    class OrderRepClass:ClassRepEntity<Order>, OrderRep
    {
        public MineVideoRental MineVideoRentalContext 
        {
            get { return dbContext as MineVideoRental; }
        }
        public OrderRepClass(MineVideoRental mineVideoRental) : base(mineVideoRental) 
        {

        }

        public void UpdateStartDate(Order order)
        {
            string dateString = Console.ReadLine();
            DateTime date = DateTime.Today;
            if (DateTime.TryParse(dateString, out date))
            {
                order.OrderStart = date;
            }
            else
                Console.WriteLine("Кривая дата. Выровните, пожалуйста");
        }

        public void UpdateEndDate(Order order)
        {
            string dateString = Console.ReadLine();
            DateTime date = DateTime.Today;
            if (DateTime.TryParse(dateString, out date))
            {
                order.OrderFinish = date;
            }
            else
                Console.WriteLine("Кривая дата. Выровните, пожалуйста");
        }

        public void UpdateFactEndDate(Order order)
        {
            string dateString = Console.ReadLine();
            DateTime date = DateTime.Today;
            if (DateTime.TryParse(dateString, out date))
            {
                order.Close(date);
            }
            else
                Console.WriteLine("Кривая дата. Выровните, пожалуйста");
        }

        public void UpdateCassetteList(Order order)
        {
            string idString = Console.ReadLine();
            int id = 0;
            if (int.TryParse(idString, out id))
            {
                var cassette = MineVideoRentalContext.Cassettes.Find(id);
                if (cassette != null)
                {
                    order.AddCassette(cassette);
                }
                else
                    Console.WriteLine("Неверный id кассетты");
            }
            else
                Console.WriteLine("Совсем неверный id кассетты");
        }
    }
}
