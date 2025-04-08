namespace _44_Task
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
            Train train;
            Route route;
            List<Carriage> carriages;

            Console.Clear();
            Console.WriteLine("Начинаем конфигурировать поезд и маршрут следования!\n");

            route = CreateRoute();
            ticketOffice.Sell();
            carriages = new(CreateCarieges(ticketOffice.TiketsSoldCount));
            train = new Train(route, carriages);
            board.AddInfo(train.Route, ticketOffice.TiketsSoldCount);

            Console.WriteLine($"Создан маршрут: \n" +
                              $"{train.GetInfo()}\n" +
                              $"\nПоезд отправлен!");
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

        private List<Carriage> CreateCarieges(int tiketsSoldCount)
        {
            List<Carriage> carriages = new List<Carriage>();
            Carriage carriage;
            int capacity = 0;

            while (capacity < tiketsSoldCount)
            {
                carriage = new();
                capacity += carriage.Capacity;
                carriages.Add(carriage);
            }

            return carriages;
        }
    }

    public class Train
    {
        private Route _route;
        private List<Carriage> _carriages;

        public Train(Route route, List<Carriage> carriages)
        {
            _carriages = new();
            _route = route;
            _carriages = carriages;
        }

        public Route Route => 
            _route;

        public string GetInfo()
        {
            return $"{_route.GetInfo()}\n" +
                   $"Состав поезда состоит из {_carriages.Count} вагонов.";
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

        public string GetInfo() =>
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

            Console.Write($"Количество проданных билетов: {TiketsSoldCount}\n");
        }
    }

    public class Board
    {
        private List<string> _trainsInfo;

        public Board()
        {
            _trainsInfo = new();
        }

        public void AddInfo(Route route, int TiketsSoldCount)
        {
            _trainsInfo.Add($"Выезд из: {route.From} по направлению в: {route.To} (Продано: {TiketsSoldCount} билетов)");
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