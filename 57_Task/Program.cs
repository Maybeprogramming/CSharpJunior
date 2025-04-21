namespace _57_Task
{
    class Program
    {
        static void Main()
        {
            Console.Title = "ДЗ: Перевод бойцов";
            Barraks barraks = new Barraks();
            barraks.Work();
            Console.ReadKey();
        }
    }

    public class Barraks
    {
        private List<Soldier> _squadOne;
        private List<Soldier> _squadTwo;

        public Barraks()
        {
            int soldiersCount = 20;
            SoldierFactory soldierFactory = new SoldierFactory();
            _squadOne = soldierFactory.CreateSoldiers(soldiersCount);
            _squadTwo = soldierFactory.CreateSoldiers(soldiersCount);
        }

        public void Work()
        {
            string conditionTransfer = "Б";

            UserUtils.Print($"Отряд 1 до трансфера", ConsoleColor.Green);
            ShowSquadInfo(_squadOne);

            UserUtils.Print($"\nОтряд 2 до трансфера", ConsoleColor.Green);
            ShowSquadInfo(_squadTwo);

            UserUtils.Print($"\nПроизводим трансфер", ConsoleColor.DarkYellow);
            TransferSoldiers(_squadOne, _squadTwo, conditionTransfer);

            UserUtils.Print($"\nОтряд 1 после трансфера", ConsoleColor.Green);
            ShowSquadInfo(_squadOne);

            UserUtils.Print($"\nОтряд 2 после трансфера", ConsoleColor.Green);
            ShowSquadInfo(_squadTwo);
        }

        private static void ShowSquadInfo(List<Soldier> soldiers)
        {
            int index = 0;
            UserUtils.Print($"\nСписок бойцов: ");

            soldiers.ForEach((soldier) =>
                UserUtils.Print($"\n{++index}. {soldier.GetInfo()}"));
        }

        private void TransferSoldiers(List<Soldier> squadOne, List<Soldier> squadTwo, string conditionTransfer)
        {
            List<Soldier> transferSoldiers = squadOne.Where(soldier => soldier.Name.StartsWith(conditionTransfer)).ToList();

            UserUtils.Print($"\nБойцы для трансфера:", ConsoleColor.Red);
            ShowSquadInfo(transferSoldiers);
            UserUtils.Print($"\n####################", ConsoleColor.Red);

            _squadOne = squadOne.Except(transferSoldiers).ToList();
            _squadTwo = squadTwo.Union(transferSoldiers).ToList();
        }
    }

    public class Soldier
    {
        public Soldier(string name, string rank)
        {
            Name = name;
            Rank = rank;
        }

        public string Name { get; }
        public string Rank { get; }

        public string GetInfo() =>
            $"{Name}, звание: <{Rank}>";
    }

    public class SoldierFactory
    {
        public List<Soldier> CreateSoldiers(int count)
        {
            List<Soldier> soldiers = new();

            for (int i = 0; i < count; i++)
            {
                soldiers.Add(CreateSoldier());
            }

            return soldiers;
        }

        private Soldier CreateSoldier()
        {
            string name = TakeRandomName();
            string rank = TakeRandomRank();

            return new Soldier(name, rank);
        }

        private string TakeRandomName()
        {
            string[] names = new[]
            {
                "Баранов", "Петров", "Сидоров", "Овечкин", "Царьков",
                "Овсянников", "Пирожков", "Галкин", "Бирюков", "Ивушкин",
                "Баландин", "Дубровский", "Ушкин", "Васильев", "Тарзанов"
            };

            return names[UserUtils.GenerateRandomNumber(0, names.Length - 1)];
        }

        private string TakeRandomRank()
        {
            string[] names = new[]
            {
                "Рядовой", "Сержант", "Полковник", "Майор", "Генерал"
            };

            return names[UserUtils.GenerateRandomNumber(0, names.Length - 1)];
        }
    }

    public static class UserUtils
    {
        private static Random s_random = new Random();

        public static int GenerateRandomNumber(int minNumber, int maxNumber) =>
            s_random.Next(minNumber, ++maxNumber);

        public static void Print<T>(T message) =>
            Console.Write(message.ToString());

        public static void Print<T>(T message, ConsoleColor consoleColor = ConsoleColor.White)
        {
            Console.ForegroundColor = consoleColor;
            Print(message);
            Console.ResetColor();
        }
    }
}