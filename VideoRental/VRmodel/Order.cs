using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VideoRental.VRmodel
{
    public class Order
    {
        public int Id { get; set; }
        public int Cost { get; set; }
        public DateTime OrderStart { get; set; }
        public DateTime OrderFinish { get; set; }
        public DateTime? OrderFactEnd { get; set; } // для возможности принимать значения null
        public int Surcharge { get; set; }

        public virtual Client Client { get; set; }// однин-ко-многим
        public virtual IList<Cassette> Cassettes { get; set; }// многие-ко-многим

        public Order()
        {
            Cassettes = new List<Cassette>();
        }

        //saving data for many-to-many
        public void AddCassette(Cassette cassette)
        {
            if (cassette.Amount <= 0)
            {
                Console.WriteLine("Товар закончился (так же как и мозги). Загляните позднее или пните соседа");
                return;
            }
            cassette.Amount--;
            Cassettes.Add(cassette);
        }

        // close order and compute surcharge
        public void Close(DateTime orderFactEnd)
        {
            OrderFactEnd = orderFactEnd;
            int days = (int)orderFactEnd.Subtract(OrderFinish).TotalDays;
            if (days > 0)
                Surcharge = days * 50;

            foreach (var cassette in Cassettes)
                cassette.Amount++;
        }

        public override string ToString()
        {
            StringBuilder stringBuild = new StringBuilder();
            stringBuild.AppendFormat($"OrderID {Id}: from {OrderStart} to ");
            if (OrderFactEnd == null)
                stringBuild.Append($"{OrderFinish}; ");
            else
                stringBuild.Append($"{OrderFactEnd}; ");
            stringBuild.Append($"Cost = {Cost}(+{Surcharge})");

            foreach (var cassette in Cassettes)
            {
                stringBuild.AppendLine();
                stringBuild.Append(cassette);
            }

            return stringBuild.ToString();
        }

        public static Order ReadData(TextReader reader = null, TextWriter writer = null)
        {
            if (reader == null)
                reader = Console.In;
            if (writer == null)
                writer = Console.Out;

            writer.WriteLine("\t Введите данные о заказе");
            writer.Write("Введите дату начала заказа (дд.мм.гггг): ");
            DateTime orderStart = new DateTime();
            string orderDate = reader.ReadLine();
            bool fail = true;

            while (!DateTime.TryParse(orderDate, out orderStart))
            {
                writer.Write("Что-то пошло не так... Попробуйте еще раз ввести дату начала заказа");
                reader.ReadLine();
                writer.Write("Введите дату начала заказа (дд.мм.гггг): ");
                orderDate = reader.ReadLine();
            }

            writer.Write("Введите дату планируемого окончания заказа (дд.мм.гггг): ");
            DateTime orderEnd = new DateTime();
            orderDate = reader.ReadLine();
            fail = true;
            while (fail)
            {
                if (DateTime.TryParse(orderDate, out orderEnd))
                {
                    if (orderEnd < orderStart)
                    {
                        writer.Write("Дата окончания должна быть больше, чем дата начала");
                        reader.ReadLine();
                        writer.Write("Введите дату планируемого окончания заказа (дд.мм.гггг): ");
                        orderDate = reader.ReadLine();
                    }
                    else
                        fail = false;
                }
                else
                {
                    writer.Write("Что-то пошло не так... Попробуйте еще раз ввести дату планируемого окончания заказа");
                    reader.ReadLine();
                    writer.Write("Введите дату планируемого окончания заказа (дд.мм.гггг): ");
                    orderDate = reader.ReadLine();

                }
            }

            int days = (int)orderEnd.Subtract(orderStart).TotalDays;
            int cost = days * 100;

            return new Order { Cost = cost, OrderStart = orderStart, OrderFinish = orderEnd };
        }
    }
}
