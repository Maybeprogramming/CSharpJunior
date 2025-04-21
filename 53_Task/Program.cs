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
            const string SortByNameCommand = "1";
            const string SortByAgeCommand = "2";
            const string ShowBySicknessCommand = "3";
            const string ShowPacientsCommand = "4";
            const string ExitCommand = "5";

            bool isWork = true;
            int pacientsCount = 30;
            List<Pacient> pacients = new PacientFactory().CreatePacients(pacientsCount);

            while (isWork)
            {
                ShowMenu(SortByNameCommand, SortByAgeCommand, ShowBySicknessCommand, ShowPacientsCommand, ExitCommand);

                switch (Console.ReadLine())
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
                        UserUtils.Print($"\nНет такой команды!", ConsoleColor.Red);
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

        private void SortByAge(List<Pacient> pacients)
        {
            pacients = pacients.OrderBy(pacient => pacient.Age).ToList();
            ShowPacients(pacients);
        }

        private void SortByName(List<Pacient> pacients)
        {
            pacients = pacients.OrderBy(pacient => pacient.Name).ToList();
            ShowPacients(pacients);
        }

        private void ShowPacients(List<Pacient> pacients)
        {
            int index = 0;
            UserUtils.Print($"Список пациентов: ", ConsoleColor.Green);

            pacients.ForEach((pacient) =>
                UserUtils.Print($"\n{++index}. {pacient.GetInfo()}"));
        }

        private void ShowMenu(string sortByNameCommand, string sortByAgeCommand, string showBySicknessCommand, string showPacientsCommand, string exitCommand)
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
        public List<Pacient> CreatePacients(int count)
        {
            List<Pacient> pacients = new();

            for (int i = 0; i < count; i++)
            {
                pacients.Add(CreatePacient());
            }

            return pacients;
        }

        private Pacient CreatePacient()
        {
            string name = TakeRandomName() + " " + TakeRandomSurName();
            string sickness = TakeRandomSickness();
            int minAge = 1;
            int maxAge = 200;
            int age = UserUtils.GenerateRandomNumber(minAge, maxAge);

            return new Pacient(name, sickness, age);
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

        private string TakeRandomSurName()
        {
            string[] surNames = new[]
            {
                "Иванов", "Петров", "Сидоров", "Овечкин", "Царьков",
                "Овсянников", "Пирожков", "Галкин", "Пугачев", "Ивушкин",
                "Настольников", "Дубровский", "Ушкин", "Васильев", "Тарзанов",
                "Авушкин", "Фролов", "Никитин", "Маркин", "Сливушкин"
            };

            return surNames[UserUtils.GenerateRandomNumber(0, surNames.Length - 1)];
        }

        private string TakeRandomSickness()
        {
            string[] sickness = new[]
            {
                "Грипп", "Язва", "Тахикардия", "Гастрит", "Панкреатит", "Ангина"
            };

            return sickness[UserUtils.GenerateRandomNumber(0, sickness.Length - 1)];
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