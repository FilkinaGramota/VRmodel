using System;
using System.Collections.Generic;
using System.IO;

namespace VideoRental.VRmodel
{
    public class Film
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }

        // many-to-many
        public virtual IList<Cassette> Cassettes { get; set; }
        public virtual IList<Genre> Genres { get; set; }

        public Film()
        {
            Cassettes = new List<Cassette>();
            Genres = new List<Genre>();
        }

        // saving data for many-to-many
        // in pink dreams - AddRange and fantastic UI and web-app with asp.net and new computer and new brain and new eyes and etc
        public void AddGenre(Genre genre)
        {
            this.Genres.Add(genre);
        }

        public override string ToString()
        {
            return $"{Title} ({Year})";
        }

        public static Film ReadData(TextReader reader = null, TextWriter writer = null)
        {
            if (reader == null)
                reader = Console.In;
            if (writer == null)
                writer = Console.Out;

            writer.Write("Введите название фильма : ");
            string title = reader.ReadLine();
            while (string.IsNullOrWhiteSpace(title))
            {
                writer.Write("Хмм... Всемирная кинобаза не может найти фильма с таким названием. Попытайтесь еще разок!");
                reader.ReadLine();
                writer.Write("Введите название фильма : ");
                title = reader.ReadLine();
            }

            writer.Write("Введите год выпуска фильма : ");
            string yearString = reader.ReadLine();
            int year = 1895;
            bool fail = true;
            while (fail)
            {
                if (int.TryParse(yearString, out year))
                {
                    if (year <= 1895 || year >= DateTime.Today.Year)
                    {
                        writer.Write("Если что, то первый фильм вышел в 1895 году. И на всякий случай внимательно посмотрите на текущий год.");
                        reader.ReadLine();
                        writer.Write("Введите год выпуска фильма : ");
                        yearString = reader.ReadLine();
                    }
                    else
                        fail = false;
                }
                else
                {
                    writer.Write("Что-то странное с вашим годом...");
                    reader.ReadLine();
                    writer.Write("Введите год выпуска фильма : ");
                    yearString = reader.ReadLine();
                }
            }

            return new Film { Title = title, Year = year};
        }
    }
}
