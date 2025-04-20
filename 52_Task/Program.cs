namespace _52_Task
{
    public class Program
    {
        static void Main()
        {
            Console.Title = "ДЗ: Амнистия";
            Prison prison = new Prison();
            prison.Work();
        }
    }

    public class Prison
    {
        public void Work()
        {
            string crimeAmnesty = "Антиправительственное";
            int criminalsCount = 30;
            List<Criminal> criminals = new CriminalFactory().GetCriminals(criminalsCount);

            Console.Clear();
            UserUtils.Print($"В нашей великой стране Арстоцка произошла амнистия!");
            UserUtils.Print($"\nВсех заключенных за <{crimeAmnesty}> преступление ожидает - Амнистия!");

            UserUtils.Print($"\nСписок заключенных до амнистии:", ConsoleColor.Green);
            ShowCriminals(criminals);

            criminals = criminals.Where(criminal => criminal.Crime != crimeAmnesty).ToList();

            UserUtils.Print($"\nСписок заключенных после амнистии:", ConsoleColor.Green);
            ShowCriminals(criminals);

            UserUtils.Print($"\nЧтобы завершить нажмите любую клавишу", ConsoleColor.Green);
            Console.ReadKey();
        }

        private void ShowCriminals(List<Criminal> criminals)
        {
            int index = 0;

            UserUtils.Print($"\nСписок заключенных:");

            foreach (Criminal criminal in criminals)
            {
                UserUtils.Print($"\n{++index}. {criminal.GetInfo()}");
            }
        }
    }

    public class Criminal
    {
        public Criminal(string name, string crime)
        {
            Name = name;
            Crime = crime;
        }

        public string Name { get; }
        public string Crime { get; }

        public string GetInfo() =>
            $"{Name}. Заключенный за: {Crime}";    
    }

    public class CriminalFactory
    {
        public List<Criminal> GetCriminals(int count)
        {
            List<Criminal> criminals = new();

            for (int i = 0; i < count; i++)
            {
                criminals.Add(CreateCrimanal());
            }

            return criminals;
        }

        private Criminal CreateCrimanal()
        {
            string name = CriminalData.TakeRandomName() + " " + CriminalData.TakeRanddomSurName();
            string crime = CriminalData.TakeRandomCrime();

            return new Criminal(name, crime);
        }
    }

    public static class CriminalData
    {
        private static string[] s_names;
        private static string[] s_surNames;
        private static string[] s_crimes;

        static CriminalData()
        {
            s_names = new string[]
            {
                "Павел", "Иван", "Сергей", "Олег", "Константин",
                "Анатолий", "Аркадий", "Петр", "Вячеслав", "Николай",
                "Владислав", "Роман", "Дмитрий", "Василий", "Михаил",
                "Руслан", "Равиль", "Фёдор", "Валерий", "Евгений"
            };

            s_surNames = new string[]
            {
                "Иванов", "Петров", "Сидоров", "Овечкин", "Царьков",
                "Овсянников", "Пирожков", "Галкин", "Пугачев", "Ивушкин",
                "Настольников", "Дубровский", "Ушкин", "Васильев", "Тарзанов",
                "Авушкин", "Фролов", "Никитин", "Маркин", "Сливушкин"
            };

            s_crimes = new string[]
            {
                "Антиправительственное", "Кражу", "Убийство", "Мошенничество"
            };
        }

        public static string TakeRandomName() =>
            GetRandomElement(s_names);

        public static string TakeRanddomSurName() =>
            GetRandomElement(s_surNames);

        public static string TakeRandomCrime() =>
           GetRandomElement(s_crimes);

        private static T GetRandomElement<T>(T[] array) =>
            array[UserUtils.GenerateRandomNumber(0, array.Length - 1)];
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

        public static int ReadInputNumber()
        {
            int result;

            while (int.TryParse(Console.ReadLine(), out result) == false)
            {
                Print($"\nВы ввели не число!\nПопробуйте снова: ", ConsoleColor.DarkYellow);
            }

            return result;
        }
    }
}