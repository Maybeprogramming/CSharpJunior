﻿namespace _44_Task
{
    public class Program
    {
        public static void Main()
        {
            Console.Title = "ДЗ: Конфигуратор пассажирских поездов";
            Station station = new();
            station.Work();
        }
    }

    public class Station
    {
        public void Work()
        {
            const string SetupTrainCommand = "1";
            const string ExitCommand = "2";

            Board board = new();
            string setupTrainMenuText = "Конфигурировать пассажирский поезд";
            string exitMenuText = "Выйти из конфигуратора";
            string menu = $"Меню:" +
                          $"\n{SetupTrainCommand} - {setupTrainMenuText}" +
                          $"\n{ExitCommand} - {exitMenuText}" +
                          $"\nВведите команду: ";
            bool isWork = true;

            while (isWork)
            {
                Console.Clear();
                board.ShowInfo();
                Console.WriteLine(menu);

                switch (Console.ReadLine())
                {
                    case SetupTrainCommand:
                        SetupTrain(board);
                        break;
                    case ExitCommand:
                        isWork = false;
                        break;
                    default:
                        Console.WriteLine("Такой команды нет, попробуйте снова");
                        break;
                }

                Console.WriteLine("\nНажмите любую клавишу чтобы продолжить...");
                Console.ReadLine();
            }
        }

        private void SetupTrain(Board board)
        {
            TicketOffice ticketOffice = new();
            Train train = new();

            Console.Clear();
            Console.WriteLine("Начинаем конфигурировать поезд и маршрут следования!\n");
            Route route = CreateRoute();

            ticketOffice.Sell();
            train.Configure(ticketOffice.TiketsSoldCount);
            board.AddInfo(route, ticketOffice);

            Console.WriteLine($"\nКонфигурирование завершено! Создан маршрут: \n" +
                              $"{route.ShowInfo()}\n" +
                              $"Состав поезда насчитывает {train.CarriagesCount} вагонов.");
            Console.WriteLine("\nПоезд отправлен!");
        }

        public Route CreateRoute()
        {
            Console.Write("Введите станцию отправления: ");
            string From = Console.ReadLine();
            string To;

            do
            {
                Console.Write("Введите станцию прибытия: ");
                To = Console.ReadLine();

                if (To == From)
                {
                    Console.WriteLine($"Станция прибытия должна отличаться от станции отправления");
                }
            }
            while (To == string.Empty || To.Equals(From));

            return new Route(From, To);
        }
    }

    public class Train
    {
        private List<Carriage> _carriages;

        public Train()
        {
            _carriages = new();
            AddCarriege();
        }

        public int Capacity { get; private set; }
        public int CarriagesCount => _carriages.Count;

        public void Configure(int tiketsSoldCount)
        {
            while (Capacity < tiketsSoldCount)
            {
                AddCarriege();
            }
        }

        private void AddCarriege()
        {
            Carriage carriage = new();
            Capacity += carriage.Capacity;
            _carriages.Add(carriage);
        }
    }

    public class Carriage
    {
        private int _minCapacity = 20;
        private int _maxCapacity = 50;

        public Carriage()
        {
            Capacity = UserUtils.GenerateRandomNumber(_minCapacity, _maxCapacity);
        }

        public int Capacity { get; }
    }

    public class Route
    {
        public Route(string from, string to)
        {
            From = from;
            To = to;
        }

        public string From { get; }
        public string To { get; }

        public string ShowInfo() =>
            $"Станция отправления: {From}, станция прибытия: {To}";
    }

    public class TicketOffice
    {
        public int TiketsSoldCount { get; private set; }

        public void Sell()
        {
            int minPassangers = 10;
            int maxPassangers = 1000;
            TiketsSoldCount = UserUtils.GenerateRandomNumber(minPassangers, maxPassangers);

            Console.Write($"Количество проданных билетов: {TiketsSoldCount}");
        }
    }

    public class Board
    {
        private List<string> _trainsInfo;

        public Board()
        {
            _trainsInfo = new();
        }

        public void AddInfo(Route route, TicketOffice ticketOffice)
        {
            _trainsInfo.Add($"Выезд из: {route.From} по направлению в: {route.To} (Продано: {ticketOffice.TiketsSoldCount} билетов)");
        }

        public void ShowInfo()
        {
            int number = 0;
            int leftCursorPosition = 0;
            int topCursorPosition = 0;

            Console.SetCursorPosition(leftCursorPosition, topCursorPosition);

            if (_trainsInfo.Count == 0)
            {
                Console.WriteLine($"Нет маршрутов для следования.\n");
                return;
            }

            topCursorPosition = 1;
            Console.WriteLine("Список текущих рейсов:");

            foreach (string info in _trainsInfo)
            {
                Console.WriteLine($"{++number}. {info}");
            }

            Console.WriteLine();

            topCursorPosition += _trainsInfo.Count + 1;
            Console.SetCursorPosition(leftCursorPosition, topCursorPosition);
        }
    }

    public static class UserUtils
    {
        private static Random s_random = new Random();

        public static int GenerateRandomNumber(int minNumber, int maxNumber) => 
            s_random.Next(minNumber, ++maxNumber);
    }
}