namespace _56_Task
{
    public class Program
    {
        static void Main()
        {
            Console.Title = "ДЗ: Отчёт о вооружении";
            Report report = new Report();
            report.Work();
        }
    }

    public class Report
    {
        public void Work()
        {
            int soldiersCount = 20;
            List<Soldier> soldiers = new SoldierFactory().CreateSoldiers(soldiersCount);

            ShowSoldiersInfo(soldiers);
            CreateInfo(soldiers);

            UserUtils.Print($"\n\nНажмите любую клавишу для продолжения", ConsoleColor.Green);
            Console.ReadKey();
        }

        private void ShowSoldiersInfo(List<Soldier> soldiers)
        {
            UserUtils.Print($"Список солдат: ", ConsoleColor.Green);

            soldiers.ForEach((soldier) =>
                UserUtils.Print($"\n{soldier.GetInfo()}"));
        }

        private void CreateInfo(List<Soldier> soldiers)
        {
            string requestRank;
            bool isRun = true;

            while (isRun == true)
            {
                UserUtils.Print($"\nВведите звание для формирования отчёта: ", ConsoleColor.Green);
                requestRank = Console.ReadLine();

                if (soldiers.Where(soldier => soldier.Rank.ToLower() == requestRank.ToLower()).Count() > 0)
                {
                    UserUtils.Print($"\nФормируем новый отчет:", ConsoleColor.Green);

                    var soldiersForRaport = soldiers.Where(soldier => 
                                                        soldier.Rank.ToLower().Equals(requestRank.ToLower()) == true)
                                                    .Select(soldier => 
                                                        new { soldier.Name, soldier.Rank }).ToList();

                    for (int i = 0; i < soldiersForRaport.Count; i++)
                    {
                        UserUtils.Print($"\n{i + 1}. {soldiersForRaport[i].Name}. Звание: {soldiersForRaport[i].Rank}");
                    }

                    isRun = false;
                }
                else
                {
                    UserUtils.Print($"\nТаких данных нет! Попробуйте снова!", ConsoleColor.Red);
                }
            }
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
            int serviceTime = UserUtils.GenerateRandomNumber(minServiceTime, maxServiceTime);

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