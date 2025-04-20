namespace _51_Task
{
    public class Program
    {
        static void Main()
        {
            Console.Title = "ДЗ: Поиск преступника";
            DetectiveOffice detectiveOffice = new DetectiveOffice();
            detectiveOffice.Work();
            Console.ReadKey();
        }
    }

    public class DetectiveOffice
    {
        public void Work()
        {
            const string FindCriminalsCommand = "1";
            const string ShowAllCriminalsCommand = "2";
            const string ExitCommand = "3";

            bool isWork = true;
            int criminalsCount = 1000;
            List<Criminal> criminals = new CriminalFactory().GetCriminals(criminalsCount);

            while (isWork)
            {
                ShowMenu(FindCriminalsCommand, ShowAllCriminalsCommand, ExitCommand);

                switch (Console.ReadLine())
                {
                    case FindCriminalsCommand:
                        FindCriminals(criminals);
                        break;
                    case ShowAllCriminalsCommand:
                        ShowCriminals(criminals);
                        break;
                    case ExitCommand:
                        isWork = false;
                        break;
                    default:
                        UserUtils.Print($"\nНет такой команды!", ConsoleColor.Red);
                        break;
                }

                UserUtils.Print($"\nДля продолжения нажмите любую клавишу", ConsoleColor.Green);
                Console.ReadKey();
            }
        }

        private void FindCriminals(List<Criminal> criminals)
        {
            int height;
            int weight;
            string nationality;
            bool isArrest = false;
            List<Criminal> foundCriminals = new List<Criminal>();

            UserUtils.Print($"\nДля поиска преступника введите следующие данные:", ConsoleColor.Green);

            UserUtils.Print("\nРост преступника: ");
            height = UserUtils.ReadInputNumber();

            UserUtils.Print("\nВес преступника: ");
            weight = UserUtils.ReadInputNumber();

            UserUtils.Print("\nНациональность преступника: ");
            nationality = Console.ReadLine();

            foundCriminals = new List<Criminal>(criminals).Where(criminal => criminal.Height == height &&
                                                                             criminal.Weight == weight &&
                                                                             criminal.Nationality.ToLower() == nationality.ToLower() &&
                                                                             criminal.IsUnderArrest == isArrest).ToList();

            if (foundCriminals.Count > 0)
            {
                ShowCriminals(foundCriminals);
            }
            else
            {
                UserUtils.Print($"\nС такими параметрами ничего не найдено", ConsoleColor.Red);
            }
        }

        private void ShowCriminals(List<Criminal> criminals)
        {
            int index = 0;

            UserUtils.Print($"Список преступников:");

            foreach (Criminal criminal in criminals)
            {
                UserUtils.Print($"\n{++index}. ");
                criminal.ShowInfo();
            }
        }

        private void ShowMenu(string findCommand, string showCriminals, string exitCommand)
        {
            Console.Clear();
            UserUtils.Print($"Команды:", ConsoleColor.Green);
            UserUtils.Print($"\n{findCommand}. Найти преступника по заданным параметрам" +
                            $"\n{showCriminals}. Показать всех преступников" +
                            $"\n{exitCommand}. Закрыть приложение");
            UserUtils.Print($"\n\nВведите команду: ", ConsoleColor.Green);
        }
    }

    public class Criminal
    {
        public Criminal(string name, string nationality, int height, int weight, bool isUnderArrest)
        {
            Name = name;
            Nationality = nationality;
            Height = height;
            Weight = weight;
            IsUnderArrest = isUnderArrest;
        }

        public string Name { get; }
        public string Nationality { get; }
        public int Height { get; }
        public int Weight { get; }
        public bool IsUnderArrest { get; }
        string ArrestStatus => IsUnderArrest == true ? "под стражей" : "на свободе";

        public string GetInfo() =>
            $"{Name}, - {Nationality}, - Рост {Height}, - Вес {Weight}, - Статус <{ArrestStatus}>";

        public void ShowInfo()
        {
            UserUtils.Print($"{Name}, Национальность <{Nationality}>, Рост <{Height}>, Вес <{Weight}>, Статус: ");
            UserUtils.Print($"<{ArrestStatus}>", IsUnderArrest == true ? ConsoleColor.Red : ConsoleColor.Green);
        }
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
            string nationality = CriminalData.TakeRandomNationality();
            int height = CriminalData.TakeRandomHeight();
            int weight = CriminalData.TakeRandomWeight();
            bool isUnderArrest = IsUnderArrestRandom();

            return new Criminal(name, nationality, height, weight, isUnderArrest);
        }

        private bool IsUnderArrestRandom()
        {
            bool[] statusArrests = new[] { true, false };

            return statusArrests[UserUtils.GenerateRandomNumber(0, statusArrests.Length - 1)];
        }
    }

    public static class CriminalData
    {
        private static string[] s_names;
        private static string[] s_surNames;
        private static int[] s_height;
        private static int[] s_weight;
        private static string[] s_nationalities;

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

            s_nationalities = new string[]
            {
                "Русский", "Татарин", "Чувашин", "Башкир", "Мордвин"
            };

            s_height = new int[]
            {
                150, 160, 170, 180, 190
            };

            s_weight = new int[]
            {
                60, 70, 80, 90, 100
            };
        }

        public static string TakeRandomName() =>
            GetRandomElement(s_names);

        public static string TakeRanddomSurName() =>
            GetRandomElement(s_surNames);

        public static int TakeRandomHeight() =>
            GetRandomElement(s_height);

        public static int TakeRandomWeight() =>
            GetRandomElement(s_weight);

        public static string TakeRandomNationality() =>
           GetRandomElement(s_nationalities);

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