using System;
using System.Collections.Generic;
using System.IO;

namespace VideoRental.VRmodel
{
    public class Genre
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public virtual IList<Film> Films { get; set; }

        public Genre()
        {
            Films = new List<Film>();
        }

        public override string ToString()
        {
            return $"Id {Id} {Type}";
        }

        public static Genre ReadData(TextReader reader = null, TextWriter writer = null)
        {
            if (reader == null)
                reader = Console.In;
            if (writer == null)
                writer = Console.Out;

            writer.Write("Введите название жанра : ");
            string type = reader.ReadLine();

            while (string.IsNullOrWhiteSpace(type))
            {
                writer.Write("Очень интересный жанр! Это тот, который в простонародье зовут черным(синим) экраном? Здесь таких не принимаем. Другой,пжлста");
                reader.ReadLine();
                writer.Write("Введите название жанра : ");
                type = reader.ReadLine();
            }

            return new Genre { Type = type };
        }
    }
}
