using System.Reflection.Emit;

namespace _56_Task
{
    public class Program
    {
        static void Main()
        {
            Console.Title = "ДЗ: Отчёт о вооружении";

            int soldiersCount = 30;
            List<Soldier> soldiers = new SoldierFactory().CreateSoldiers(soldiersCount);

            soldiers.ForEach((soldier) => 
                UserUtils.Print($"\n{soldier.GetInfo()}"));


        }
    }

    public class Soldier
    {
        public Soldier(string name, string weapon, string rank, int serviceTime)
        {
            Name = name;
            Weapon = weapon;
            Rank = rank;
            ServiceTime = serviceTime;
        }

        public string Name { get; }
        public string Weapon { get; }
        public string Rank { get; }
        public int ServiceTime { get; }

        public string GetInfo() =>
            $"{Name}, звание: <{Rank}>, вооружение: <{Weapon}>, время службы: <{ServiceTime}>";
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
            string weapon = TakeRandomWeapon();
            int minServiceTime = 1;
            int maxServiceTime = 36;
            int serviceTime =  UserUtils.GenerateRandomNumber(minServiceTime, maxServiceTime);

            return new Soldier(name, weapon, rank, serviceTime);
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

        private string TakeRandomWeapon()
        {
            string[] names = new[]
            {
                "Пистолет", "Винтовка", "Автомат", "Пулемет", "FPV-дрон"
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