namespace _36_Task
{
    public class Program
    {
        private static void Main()
        {
            Console.Title = "ДЗ: Кадровый учет продвинутый";
            Work();
        }

        private static void Work()
        {
            const int CommandAddDossier = 1;
            const int CommandRemoveDossier = 2;
            const int CommandShowDossiers = 3;
            const int CommandExit = 4;

            Dictionary<string, List<string>> dossiers = GetRandomDossiers(5);
            bool isRun = true;
            int userInput;

            while (isRun)
            {
                Console.Clear();
                Console.WriteLine(
                    $"Меню:" +
                    $"\n{CommandAddDossier} - Добавить досье" +
                    $"\n{CommandRemoveDossier} - Удалить досье" +
                    $"\n{CommandShowDossiers} - Показать все досье" +
                    $"\n{CommandExit} - Выход");

                if (int.TryParse(Console.ReadLine(),out userInput))
                {
                    switch (userInput)
                    {
                        case CommandAddDossier:
                            AddDossier(dossiers);
                            break;

                        case CommandRemoveDossier:
                            RemoveDossier(dossiers);
                            break;

                        case CommandShowDossiers:
                            ShowDossiers(dossiers);
                            break;
                        case CommandExit:
                            isRun = false;
                            break;

                        default:
                            Console.WriteLine("Такой команды нет, попробуйте ввести другую");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка ввода, попробуйте ещё раз");
                }
            }

            Console.ReadLine();
        }

        private static void RemoveDossier(Dictionary<string, List<string>> dossiers)
        {
            Console.WriteLine("Удаление");
        }

        private static void AddDossier(Dictionary<string, List<string>> dossiers)
        {
            Console.WriteLine("Добавление");
        }

        private static void ShowDossiers(Dictionary<string, List<string>> dossiers)
        {
            int indexPosition = 0;

            foreach (var keyPosition in dossiers.Keys)
            {
                List<string> fullNamesEmployers = dossiers[keyPosition];

                foreach (var fullName in fullNamesEmployers)
                {
                    Console.WriteLine($"{++indexPosition}. {keyPosition} - {fullName}");
                }
            }
        }

        private static Dictionary<string, List<string>> GetRandomDossiers(int amountDossiers)
        {
            Random random = new Random();
            Dictionary<string, List<string>> dossiers = new();
            List<string> positions = new List<string>() { "Инженер", "Электрик", "Машинист", "Директор", "Бухгалтер" };
            List<string> names = new List<string>() { "Алексей", "Иван", "Олег", "Сергей", "Роман" };
            List<string> surNames = new List<string>() { "Геннадьевич", "Николаевич", "Михайлович", "Алексеевич", "Олегович" };
            List<string> lastNames = new List<string>() { "Иванов", "Петров", "Бычков", "Андреев", "Тимашков" };

            for (int i = 0; i < amountDossiers; i++)
            {
                string position = GetRandomString(positions, random);
                string fullName = GetRandomEmployer(names, surNames, lastNames, random);

                if (dossiers.ContainsKey(position) == false)
                {
                    List<string> employers = new();
                    employers.Add(fullName);
                    dossiers.Add(position, employers);
                }
                else
                {
                    List<string> employers = dossiers[position];
                    employers.Add(fullName);
                }
            }

            return dossiers;
        }

        private static string GetRandomEmployer(List<string> names, List<string> surNames, List<string> lastNames, Random random) =>
            GetRandomString(names, random) + " " + GetRandomString(surNames, random) + " " + GetRandomString(lastNames, random);

        private static string GetRandomString(List<string> strings, Random random) =>
            strings[random.Next(strings.Count)];
    }
}