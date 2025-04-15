namespace _48_Task
{
    public class Program
    {
        static void Main()
        {
            Console.Title = "ДЗ: Аквариум";
            Aquarium aquarium = new Aquarium();
            aquarium.Work();
        }
    }

    public class Aquarium
    {
        private List<Fish> _fishes;
        private int _maxFishCount;

        public Aquarium()
        {
            _fishes = new List<Fish>() 
            {
                new Fish(),
                new Fish(),
                new Fish(),
                new Fish(),
                new Fish()
            };

            _maxFishCount = UserUtils.GenerateRandomNumber(7, 15);
        }

        public void Work()
        {
            const string AddFishCommand = "1";
            const string RemoveFishCommand = "2";
            const string ExitCommand = "3";

            bool isRun = true;

            while (isRun)
            {
                Console.Clear();
                ShowFishes();

                UserUtils.Print($"\n\nДоступные команды:" +
                    $"\n{AddFishCommand} - добавить рыбку в аквариум" +
                    $"\n{RemoveFishCommand} - убрать мертвых рыбок из аквариума" +
                    $"\n{ExitCommand} - завершить программу");

                UserUtils.Print($"\n\nДля следующего цикла нажмите любую клавишу или", ConsoleColor.Green);
                UserUtils.Print($"\nВведите номер для выполнения команды: ", ConsoleColor.Green);

                switch (Console.ReadLine())
                {
                    case AddFishCommand:
                        AddFish();
                        break;
                    case RemoveFishCommand:
                        RemoveDeadFish();
                        break;
                    case ExitCommand:
                        isRun = false;
                        break;
                    default:
                        IncreaseAge();
                        break;
                }

                UserUtils.Print($"\n\nНажмите любую клавишу чтобы продолжить", ConsoleColor.Green);
                Console.ReadLine();
            }
        }

        private void IncreaseAge()
        {
            foreach (Fish fish in _fishes)
            {
                fish.IncreaseAge();
            }

            UserUtils.Print($"\nПрошёл 1 цикл жизни");
        }

        private void AddFish() 
        {
            if (_fishes.Count < _maxFishCount)
            {
                Fish fish = new Fish();
                _fishes.Add(fish);

                UserUtils.Print($"\nВ аквариум добавили рыбку:" +
                                $"\n{fish.GetInfo()}", ConsoleColor.Green);
            }
            else
            {
                UserUtils.Print($"\nАквариум полон, вы не можете больше добавить рыбок", ConsoleColor.Red);
            }
        }

        private void RemoveDeadFish() => 
            _fishes.RemoveAll(fishes => fishes.IsAlive == false);

        private void ShowFishes()
        {
            if (_fishes.Count > 0) 
            {
                int index = 0;
                UserUtils.Print($"Список рыбок в аквариуме:", ConsoleColor.Green);

                foreach (Fish fish in _fishes)
                {
                    UserUtils.Print($"\n{++index}. {fish.GetInfo()}");
                }
            }
            else
            {
                UserUtils.Print($"Аквариум пуст", ConsoleColor.Green);
            }
        }
    }

    public class Fish
    {
        private int _age;
        private int _maxAge;

        public Fish()
        {
            _maxAge = UserUtils.GenerateRandomNumber(10, 15);
            _age = UserUtils.GenerateRandomNumber(0, 5);
        }

        public int Age
        {
            get => _age;
            private set => _age = SetAge(value);
        }

        public bool IsAlive => 
            Age < _maxAge;

        public string AliveStatus => 
            IsAlive ? "жива" : "мертва";

        public void IncreaseAge() => 
            Age++;

        public string GetInfo() => 
            $"Рыбка возратом <{Age}>, статус: <{AliveStatus}>";

        private int SetAge(int value)
        {
            if (value >= _maxAge)
            {
                return _maxAge;
            }
            else if (value < 0)
            {
                return 0;
            }
            else
            {
                return value;
            }
        }
    }

    public static class UserUtils
    {
        private static Random s_random = new Random();

        public static int GenerateRandomNumber(int minNumber, int maxNumber) =>
            s_random.Next(minNumber, maxNumber);

        public static void Print<T>(T message) =>
            Console.Write(message.ToString());

        public static void Print<T>(T message, ConsoleColor consoleColor = ConsoleColor.White)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = consoleColor;
            Print(message);
            Console.ForegroundColor = defaultColor;
        }
    }
}