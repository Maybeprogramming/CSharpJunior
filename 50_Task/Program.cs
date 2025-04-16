﻿namespace _50_Task
{
    namespace AquariumTask
    {
        public class Program
        {
            static void Main()
            {
                Console.Title = "ДЗ: Аквариум";
                new Aquarium().Work();
            }
        }

        public class Aquarium
        {
            private readonly List<Fish> _fishes;
            private readonly int _maxFishCount;
            private readonly FishFactory _fishFactory = new FishFactory();

            public Aquarium()
            {
                int fishCount = UserUtils.GenerateRandomNumber(3, 10);
                _fishes = _fishFactory.GetFishes(fishCount);
                _maxFishCount = UserUtils.GenerateRandomNumber(7, 15);
            }

            public void Work()
            {
                const string AddFishCommand = "1";
                const string RemoveFishCommand = "2";
                const string ExitCommand = "3";

                bool isRunning = true;

                while (isRunning)
                {
                    Console.Clear();
                    ShowFishes();

                    UserUtils.PrintMenu(
                        $"\nДоступные команды:",
                        $"{AddFishCommand} - добавить рыбку в аквариум",
                        $"{RemoveFishCommand} - убрать мертвых рыбок",
                        $"{ExitCommand} - завершить программу"
                    );

                    string input = Console.ReadLine();
                    ProcessCommand(input, ref isRunning);

                    UserUtils.Print("\nНажмите любую клавишу чтобы продолжить...", ConsoleColor.Green);
                    Console.ReadKey();
                }
            }

            private void ProcessCommand(string input, ref bool isRunning)
            {
                switch (input)
                {
                    case "1":
                        AddFish();
                        break;
                    case "2":
                        RemoveDeadFish();
                        break;
                    case "3":
                        isRunning = false;
                        break;
                    default:
                        IncreaseAge();
                        break;
                }
            }

            private void IncreaseAge()
            {
                _fishes.ForEach(fish => fish.IncreaseAge()); //И тут фо ич с лямбда!
                UserUtils.Print("\nПрошёл 1 цикл жизни");
            }

            private void AddFish()
            {
                if (_fishes.Count >= _maxFishCount)
                {
                    UserUtils.Print("\nАквариум полон, вы не можете больше добавить рыбок", ConsoleColor.Red);
                    return;
                }

                var fish = new Fish();
                _fishes.Add(fish);
                UserUtils.Print($"\nВ аквариум добавили рыбку:\n{fish.GetInfo()}", ConsoleColor.Green);
            }

            private void RemoveDeadFish() => _fishes.RemoveAll(fish => !fish.IsAlive);

            private void ShowFishes()
            {
                int index = 0;
                if (_fishes.Count == 0)
                {
                    UserUtils.Print("Аквариум пуст", ConsoleColor.Green);
                    return;
                }

                UserUtils.Print("Список рыбок в аквариуме:", ConsoleColor.Green);
                _fishes.ForEach((fish) =>
                    UserUtils.Print($"\n{index + 1}. {fish.GetInfo()}"));  //Конструкция ForEach с Action интересна!
            }
        }

        public class FishFactory
        {
            public List<Fish> GetFishes(int count) =>
                Enumerable.Range(0, count).Select(_ => new Fish()).ToList(); //И это интересно!
        }

        public class Fish
        {
            private int _age;
            private readonly int _maxAge;

            public Fish()
            {
                _maxAge = UserUtils.GenerateRandomNumber(10, 15);
                _age = UserUtils.GenerateRandomNumber(0, 5);
            }

            public int Age
            {
                get => _age;
                private set => _age = Math.Clamp(value, 0, _maxAge); //!!! Вот это интересно
            }

            public bool IsAlive => Age < _maxAge;
            public string AliveStatus => IsAlive ? "жива" : "мертва";

            public void IncreaseAge() => Age++;
            public string GetInfo() => $"Рыбка возрастом {Age}, статус: {AliveStatus}";
        }

        public static class UserUtils
        {
            private static readonly Random Random = new();

            public static int GenerateRandomNumber(int min, int max) => Random.Next(min, max);

            public static void Print(object message, ConsoleColor color = ConsoleColor.White)
            {
                Console.ForegroundColor = color;
                Console.Write(message);
                Console.ResetColor(); // Это норм!
            }

            public static void PrintMenu(params string[] menuItems)
            {
                foreach (var item in menuItems)
                {
                    Print(item + "\n", ConsoleColor.Green);
                }
                Print("\nВведите номер команды: ", ConsoleColor.Green);
            }
        }
    }
}