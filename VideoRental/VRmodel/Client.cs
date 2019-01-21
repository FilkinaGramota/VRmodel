using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VideoRental.VRmodel
{
    public class Client
    {
        public int Id { get; set; }
        public Name Name { get; set; } // complex type

        public virtual IList<Order> Orders { get; set; }
        
        public Client()
        {
            Orders = new List<Order>();
        }

        public void AddOrder(Order order)
        {
            Orders.Add(order);
        }

        public override string ToString()
        {
            StringBuilder stringBuild = new StringBuilder();
            stringBuild.AppendFormat($"Id {Id} {Name}; список заказов: ");

            foreach (var order in Orders)
            {
                stringBuild.AppendLine();
                stringBuild.Append(order);
            }
            stringBuild.AppendLine();

            return stringBuild.ToString();
        }

        public static Client ReadData(TextReader reader = null, TextWriter writer = null)
        {
            if (reader == null)
                reader = Console.In;
            if (writer == null)
                writer = Console.Out;

            writer.WriteLine("\t Введите данные о клиенте");

            writer.Write("Имя : ");
            string firstName = reader.ReadLine();
            while (string.IsNullOrWhiteSpace(firstName))
            {
                writer.Write("С пустотами не общаемся. Пожалуйста, введите хоть какое-нибудь имя.");
                reader.ReadLine();
                writer.Write("Имя : ");
                firstName = reader.ReadLine();
            }

            writer.Write("Фамилия : ");
            string lastName = reader.ReadLine();
            while (string.IsNullOrWhiteSpace(lastName))
            {
                writer.Write("С пустотами не общаемся. Пожалуйста, введите хоть какую-нибудь фамилию.");
                reader.ReadLine();
                writer.Write("Фамилия : ");
                lastName = reader.ReadLine();
            }

            return new Client { Name = new Name(firstName, lastName) };
        }
    }
}
