using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VideoRental.VRmodel
{
    public class Cassette
    {
        public int Id { get; set; }
        public string Title { get; set; } //чаще всего будет совпадать с названием фильма, но... Как пойдет
        public int Amount { get; set; }

        // virtual - для обеспечения выполнения ленивой загрузки
        public virtual IList<Film> Films { get; set; }
        public virtual IList<Order> Orders { get; set; }

        public Cassette()
        {
            Films = new List<Film>();
            Orders = new List<Order>();
        }

        //saving data for many-to-many
        public void AddFilm(Film film)
        {
            this.Films.Add(film);
        }

        public override string ToString()
        {
            StringBuilder stringBuild = new StringBuilder();
            
            stringBuild.AppendFormat($"{Title} : ");
            foreach (var film in Films)
                stringBuild.Append($"{film}; ");

            return stringBuild.ToString();
        }

        public static Cassette ReadData(TextReader reader = null, TextWriter writer = null)
        {
            if (reader == null)
                reader = Console.In;
            if (writer == null)
                writer = Console.Out;

            writer.Write("Введите название кассетты: ");
            string title = reader.ReadLine();
            while (string.IsNullOrWhiteSpace(title))
            {
                writer.Write("Хоть как-то называться кассетта должна же! Включите зрение (или воображение) и попробуйте еще раз.");
                reader.ReadLine();
                writer.Write("Введите название кассетты : ");
                title = reader.ReadLine();
            }

            writer.Write("Введите количество экземпляров : ");
            int amount = 0;
            string amountString = reader.ReadLine();

            while (!int.TryParse(amountString, out amount))
            {
                writer.Write("Что-то пошло не так... Попытайтесь еще раз ввести количество кассет");
                reader.ReadLine();
                writer.Write("Введите количество экземпляров : ");
                amountString = reader.ReadLine();
            }

            return new Cassette { Title = title, Amount = amount };
        }
    }
}
