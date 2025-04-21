namespace _53_Task
{
    public class Program
    {
        static void Main()
        {
            Console.Title = "ДЗ: Анархия в больнице";
            Hospital hospital = new Hospital();
            hospital.Work();
        }
    }

    public class Hospital
    {
        public void Work()
        {
            const int SortByNameCommand = 1;
            const int SortByAgeCommand = 2;
            const int ShowBySicknessCommand = 3;
            const int ShowPacientsCommand = 4;
            const int ExitCommand = 5;

            bool isWork = true;
            int pacientsCount = 30;
            List<Pacient> pacients = new PacientFactory().GetPacients(pacientsCount);

            while (isWork)
            {
                ShowMenu(SortByNameCommand, SortByAgeCommand, ShowBySicknessCommand, ShowPacientsCommand, ExitCommand);

                switch (UserUtils.ReadInputNumber())
                {
                    case SortByNameCommand:
                        SortByName(pacients);
                        break;
                    case SortByAgeCommand:
                        SortByAge(pacients);
                        break;
                    case ShowBySicknessCommand:
                        ShowBySickness(pacients);
                        break;
                    case ShowPacientsCommand:
                        ShowPacients(pacients);
                        break;
                    case ExitCommand:
                        isWork = false;
                        break;
                    default:
                        break;
                }

                UserUtils.Print($"\nДля продолжения нажмите любую клавишу", ConsoleColor.Green);
                Console.ReadKey();
            }
        }

        private void ShowBySickness(List<Pacient> pacients)
        {
            string inputSickness;
            List<Pacient> pacientsBySickness;

            do
            {
                UserUtils.Print($"\nВведите наименование заболевания для вывода пациентов: ", ConsoleColor.Green);
                inputSickness = Console.ReadLine();

                pacientsBySickness = pacients.Where(pacient => pacient.Sickness.ToLower() == inputSickness.ToLower()).ToList();
            }
            while (pacientsBySickness.Count == 0);

            ShowPacients(pacientsBySickness);
        }

        private void SortByAge(List<Pacient> pacients) =>
            SortByParametr<Pacient>(pacients, pacient => pacient.Age.ToString());

        private void SortByName(List<Pacient> pacients) =>
            SortByParametr<Pacient>(pacients, pacient => pacient.Name);

        private void SortByParametr<T>(List<Pacient> pacients, Func<Pacient, string> sortSelector)
        {
            pacients = pacients.OrderBy(sortSelector).ToList();
            ShowPacients(pacients);
        }

        private void ShowPacients(List<Pacient> pacients)
        {
            int index = 0;
            UserUtils.Print($"Список пациентов: ", ConsoleColor.Green);

            pacients.ForEach((pacient) =>
                UserUtils.Print($"\n{++index}. {pacient.GetInfo()}"));
        }

        private void ShowMenu(int sortByNameCommand, int sortByAgeCommand, int showBySicknessCommand, int showPacientsCommand, int exitCommand)
        {
            Console.Clear();
            UserUtils.Print($"Команды:", ConsoleColor.Green);
            UserUtils.Print($"\n{sortByNameCommand}. Отсортировать пациентов по имени" +
                            $"\n{sortByAgeCommand}. Отсортировать пациентов по возрасту" +
                            $"\n{showBySicknessCommand}. Найти пациентов по заболеванию" +
                            $"\n{showPacientsCommand}. Показать всех пациентов");
            UserUtils.Print($"\n{exitCommand}. Закрыть программу", ConsoleColor.Red);

            UserUtils.Print($"\nВведите команду: ", ConsoleColor.Green);
        }
    }

    public class Pacient
    {
        public Pacient(string name, string sickness, int age)
        {
            Name = name;
            Sickness = sickness;
            Age = age;
        }

        public string Name { get; }
        public string Sickness { get; }
        public int Age { get; }

        public string GetInfo() =>
            $"{Name}. Возраст: <{Age}> лет, Заболевание: <{Sickness}>";
    }

    public class PacientFactory
    {
        public List<Pacient> GetPacients(int count)
        {
            List<Pacient> criminals = new();

            for (int i = 0; i < count; i++)
            {
                criminals.Add(CreatePacient());
            }

            return criminals;
        }

        private Pacient CreatePacient()
        {
            string name = PacientData.TakeRandomName() + " " + PacientData.TakeRanddomSurName();
            string sickness = PacientData.TakeRandomSickness();
            int minAge = 16;
            int maxAge = 80;
            int age = UserUtils.GenerateRandomNumber(minAge, maxAge);

            return new Pacient(name, sickness, age);
        }
    }

    public static class PacientData
    {
        private static string[] s_names;
        private static string[] s_surNames;
        private static int[] s_age;
        private static string[] s_sickness;

        static PacientData()
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

            s_sickness = new string[]
            {
                "Грипп", "Язва", "Тахикардия", "Гастрит", "Панкреатит", "Ангина"
            };
        }

        public static string TakeRandomName() =>
            GetRandomElement(s_names);

        public static string TakeRanddomSurName() =>
            GetRandomElement(s_surNames);

        public static string TakeRandomSickness() =>
           GetRandomElement(s_sickness);

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