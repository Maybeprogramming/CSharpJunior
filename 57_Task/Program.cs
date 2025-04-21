namespace _57_Task
{
    class Program
    {
        static void Main()
        {
            Console.Title = "ДЗ: Перевод бойцов";
        }
    }

    public class Soldier
    {
        public Soldier(string name,string rank)
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
                "Павел", "Иван", "Сергей", "Олег", "Константин",
                "Анатолий", "Аркадий", "Петр", "Вячеслав", "Николай",
                "Владислав", "Роман", "Дмитрий", "Василий", "Михаил",
                "Руслан", "Равиль", "Фёдор", "Валерий", "Евгений"
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