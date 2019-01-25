using VideoRental.VRmodel;
using System;

namespace VideoRental
{
    class Program
    {
        public static void AddNewCassette()
        {
            Cassette cassette = Cassette.ReadData();
            // Добавление фильмов
            using (var unit = new ClassUnitOfWorkRep(new MineVideoRental()))
            {
                // если с таким названием кассетта уже есть
                if (unit.CassetteRepasitory.IsCassetteExists(cassette))
                {
                    Console.WriteLine("Ничего нового - такая кассетта уже есть");
                    Console.ReadLine();
                    unit.Dispose();
                    return; // то просто выходим из метода
                }

                bool enough = false;
                bool onceMore = true;
                Console.WriteLine("Добавить фильмы:");
                do
                {
                    string answerString;
                    int answer = 0;

                    Film film = Film.ReadData();
                    if (unit.FilmRepasitory.IsFilmExists(film)) // если такой фильм уже есть
                    {
                        film = unit.FilmRepasitory.GetSame(film); // то берем его из базы
                        onceMore = false; // еще жанры к фильму не будем добавлять
                    }
                    else
                    {
                        Genre genre = Genre.ReadData();
                        if (unit.GenreRepasitory.IsGenreExists(genre)) // если такой жанр уже есть
                            genre = unit.GenreRepasitory.GetSame(genre); // то берем его из базы
                        else
                            unit.GenreRepasitory.Add(genre);

                        film.AddGenre(genre);
                        unit.FilmRepasitory.Add(film);
                        onceMore = true;
                    }

                    while(onceMore)
                    {
                        Console.Write("Добавить еще жанр к фильму? (0 - нет, 1 - да) ");
                        answerString = Console.ReadLine();
                        
                        if (int.TryParse(answerString, out answer))
                        {
                            if (answer != 0)
                            {
                                Genre genre = Genre.ReadData();
                                if (unit.GenreRepasitory.IsGenreExists(genre))
                                    genre = unit.GenreRepasitory.GetSame(genre);
                                else
                                    unit.GenreRepasitory.Add(genre);
                                // update
                                film.AddGenre(genre);
                            }
                            else
                                onceMore = false;
                        }
                        else
                        {
                            Console.WriteLine("Что-то пошло не так... Ничего не будем добавлять");
                            onceMore = false;
                        }

                    }

                    cassette.AddFilm(film);

                    Console.Write("Добавить еще фильм? (0 - нет, 1 - да) ");
                    answerString = Console.ReadLine();
                    answer = 0;
                    if (int.TryParse(answerString, out answer))
                    {
                        if (answer == 0)
                            enough = true;
                    }

                } while (!enough);

                unit.CassetteRepasitory.Add(cassette);
                unit.Save();
            }
        }

        public static void AddNewOrder()
        {
            Order order = Order.ReadData();

            using (var unit = new ClassUnitOfWorkRep(new MineVideoRental()))
            {
                var cassettes = unit.CassetteRepasitory.GetCassetteMax(0);// cassettes which have Amount > 0
                Console.WriteLine("Список кассет, имеющихся в базе: \n");
                foreach (var cassette in cassettes)
                {
                    Console.WriteLine($"{cassette}\n");
                }

                Console.WriteLine("Введите через пробел номера Id, которые будут в заказе: (например, 2 13 8)");
                string IdString = Console.ReadLine();
                var splittingStrings = IdString.Split(' ');

                int id = 0;
                foreach (var s in splittingStrings)
                {
                    if (int.TryParse(s, out id))
                    {
                        var cassette = unit.CassetteRepasitory.Get(id);
                        order.AddCassette(cassette);
                    }
                }

                Console.WriteLine("Список клиентов, имеющихся в базе: \n");
                var clients = unit.ClientRepasitory.GetAll();
                foreach (var client in clients)
                    Console.WriteLine($"{client.Id}. {client.Name}");

                Console.Write("\nВведите Id клиента или 0 для добавления нового клиента: ");
                IdString = Console.ReadLine();
                if (int.TryParse(IdString, out id))
                {
                    if (id != 0)
                    {
                        var client = unit.ClientRepasitory.Get(id);
                        client.AddOrder(order);
                    }
                    else
                    {
                        var client = Client.ReadData();
                        if (unit.ClientRepasitory.IsClientExists(client))
                        {
                            Console.WriteLine("Ку! Есть уже такой клиент, к нему и добавим заказ");
                            client = unit.ClientRepasitory.GetSame(client);
                        }
                        else
                            unit.ClientRepasitory.Add(client);

                        client.AddOrder(order);
                        unit.OrderRepasitory.Add(order);
                        unit.Save();
                    }
                }

            }
        }

        public static void ShowCassettes()
        {
            using (var unit = new ClassUnitOfWorkRep(new MineVideoRental()))
            {
                var cassettes = unit.CassetteRepasitory.GetAll();
                foreach (var cassette in cassettes)
                {
                    Console.WriteLine(cassette);
                    Console.WriteLine();
                }
            }
        }

        public static void ShowClientOrders()
        {
            using (var unit = new ClassUnitOfWorkRep(new MineVideoRental()))
            {
                var clients = unit.ClientRepasitory.GetAll();
                foreach (var client in clients)
                    Console.WriteLine(client);
            }
        }

        public static void DeleteCassette()
        {
            using (var unit = new ClassUnitOfWorkRep(new MineVideoRental()))
            {
                var cassettes = unit.CassetteRepasitory.GetAll();
                foreach (var cassette in cassettes)
                    Console.WriteLine($"{cassette} \n");

                Console.Write("Введите Id кассетты, которую больше не желаете видеть в базе: ");
                string idString = Console.ReadLine();
                int id = 0;
                if (int.TryParse(idString, out id))
                {
                    var cassette = unit.CassetteRepasitory.Get(id);
                    if (cassette != null)
                    {
                        unit.CassetteRepasitory.Delete(cassette);
                        unit.Save();
                        Console.WriteLine($"Кассетта c Id = {id} удалена из базы");
                    }
                    else
                    {
                        Console.WriteLine($"Не нашлась кассетта с Id = {id}");
                    }
                }
                else
                {
                    Console.WriteLine("Тяжко с Вами... Id - это номер, число, цифра (арабская, если что)");
                }
            }
        }

        public static void UpdateOrder()
        {
            using (var unit = new ClassUnitOfWorkRep(new MineVideoRental()))
            {
                var orders = unit.OrderRepasitory.GetAll();
                foreach (var order in orders)
                    Console.WriteLine($"{order.Id}. {order}\n");

                Console.Write("Введите Id заказа, который требуется изменить: ");
                string idString = Console.ReadLine();
                int id = 0;

                if (int.TryParse(idString, out id))
                {
                    var order = unit.OrderRepasitory.Get(id);
                    if (order != null)
                    {
                        Console.Write("Изменить дату начала заказа (0 - нет, 1 - да): ");
                        string answerString = Console.ReadLine();
                        int answer = 0;
                        if (int.TryParse(answerString, out answer))
                        {
                            if (answer == 1)
                            {
                                unit.OrderRepasitory.UpdateStartDate(order);
                                unit.Save();
                            }
                        }

                        Console.Write("Изменить дату планируемого окончания заказа (0 - нет, 1 - да): ");
                        answerString = Console.ReadLine();
                        if (int.TryParse(answerString, out answer))
                        {
                            if (answer == 1)
                            {
                                unit.OrderRepasitory.UpdateEndDate(order);
                                unit.Save();
                            }
                        }

                        Console.Write("Изменить дату фактического окончания заказа (0 - нет, 1 - да): ");
                        answerString = Console.ReadLine();
                        if (int.TryParse(answerString, out answer))
                        {
                            if (answer == 1)
                            {
                                unit.OrderRepasitory.UpdateFactEndDate(order);
                                unit.Save();
                            }
                        }

                        Console.Write("Добавить еще одну кассетту в заказ (0 - нет, 1 - да): ");
                        answerString = Console.ReadLine();
                        if (int.TryParse(answerString, out answer))
                        {
                            if (answer == 1)
                            {
                                unit.OrderRepasitory.UpdateCassetteList(order);
                                unit.Save();
                            }
                        }
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            UserInput choice;

            do
            {
                Console.Clear();
                choice = Menu.UserChoice();

                switch (choice)
                {
                    case UserInput.AddNewCassette:
                        Console.WriteLine("\tДобавление в базу новой кассетты:");
                        AddNewCassette();
                        Console.WriteLine("Кассетта добавлена в базу. Для продолжения ткните в клавиатуру.");
                        Console.ReadLine();
                        break;

                    case UserInput.AddNewOrder:
                        Console.WriteLine("\tФормирование нового заказа:");
                        AddNewOrder();
                        Console.WriteLine("Заказ сформирован. Для продолжения ткните в клавиатуру.");
                        Console.ReadLine();
                        break;

                    case UserInput.DeleteCassette:
                        Console.WriteLine("\tУдалить из базы кассетту:");
                        DeleteCassette();
                        Console.ReadLine();
                        break;

                    case UserInput.UpdateOrder:
                        Console.WriteLine("\tОбновление заказа:");
                        UpdateOrder();
                        Console.ReadLine();
                        break;

                    case UserInput.ShowClientOrders:
                        Console.WriteLine("\tПросмотр клиентов и их заказов:");
                        ShowClientOrders();
                        Console.ReadLine();
                        break;

                    case UserInput.ShowCassettes:
                        Console.WriteLine("\tСписок кассетт:\n");
                        ShowCassettes();
                        Console.ReadLine();
                        break;

                    case UserInput.Exit:
                        Console.WriteLine("\t И Вам не хворать!");
                        Console.ReadLine();
                        break;

                    default:
                        Console.WriteLine("Сконцентрируйтесь...");
                        Console.ReadLine();
                        break;
                }
            } while (choice != UserInput.Exit);
        }
    }
}
