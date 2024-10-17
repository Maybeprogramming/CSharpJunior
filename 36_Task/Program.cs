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

            int randomEmployes = 10;
            Dictionary<string, List<string>> dossiers = GetRandomDossiers(randomEmployes);
            bool isRun = true;
            int userInput;
            bool isNumber;

            while (isRun)
            {
                Console.Clear();
                Console.WriteLine(
                    $"Меню:" +
                    $"\n{CommandAddDossier} - Добавить досье" +
                    $"\n{CommandRemoveDossier} - Удалить досье" +
                    $"\n{CommandShowDossiers} - Показать все досье" +
                    $"\n{CommandExit} - Выход");

                isNumber = int.TryParse(Console.ReadLine(), out userInput);

                if (isNumber == false)
                {
                    Console.WriteLine("Вы ввели не число!");
                    Console.ReadLine();
                    continue;
                }

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
                        Console.WriteLine("Команды с таким номером нет!");
                        break;
                }

                Console.ReadLine();
            }
        }

        private static void RemoveDossier(Dictionary<string, List<string>> dossiers)
        {
            int indexEmployee = 0;
            string inputPosition;
            int inputNumber;

            ShowPositions(dossiers);

            inputPosition = ReadUserInputText("\nВведите должность:");

            if (dossiers.ContainsKey(inputPosition) == false)
            {
                Console.WriteLine("Такой должности нет!!");
                return;
            }

            Console.WriteLine($"В должности [{inputPosition}] следующие сотрудники:");

            foreach (string employee in dossiers[inputPosition])
            {
                Console.WriteLine($"{++indexEmployee}. {employee}");
            }

            Console.WriteLine($"\nВведите номер сотрудника для удаления:");

            if (int.TryParse(Console.ReadLine(), out inputNumber) == false)
            {
                Console.WriteLine("Вы ввели не число!");
                return;
            }

            if (inputNumber < 0 && inputNumber >= dossiers[inputPosition].Count)
            {
                Console.WriteLine("Сотрудника с таким номером нет в списке!");
                return;
            }

            inputNumber--;
            Console.WriteLine($"\n{dossiers[inputPosition][inputNumber]} - успешно удалён из базы!");
            dossiers[inputPosition].RemoveAt(inputNumber);

            CheckPositionForEmpty(dossiers, inputPosition);
        }

        private static void CheckPositionForEmpty(Dictionary<string, List<string>> dossiers, string position)
        {
            if (dossiers[position].Count == 0)
            {
                dossiers.Remove(position);
                Console.WriteLine($"Удалена должность: {position}, так как нет сотрудников в этой должности!");
            }
        }

        private static void AddDossier(Dictionary<string, List<string>> dossiers)
        {
            string inputPosition = ReadUserInputText("Введите должность:");
            string inputEmployeeName = ReadUserInputText("Введите ФИО сотрудника:");

            if (dossiers.ContainsKey(inputPosition) == false)
            {
                List<string> employes = new();
                dossiers.Add(inputPosition, employes);
            }

            dossiers[inputPosition].Add(inputEmployeeName);

            Console.WriteLine("Сотрудник успешно добавлен в базу!");
        }

        private static string ReadUserInputText(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }

        private static void ShowPositions(Dictionary<string, List<string>> dossiers)
        {
            Console.Clear();
            Console.WriteLine("Список всех должностей:");

            foreach (string potition in dossiers.Keys)
            {
                Console.WriteLine($"{potition}");
            }
        }

        private static void ShowDossiers(Dictionary<string, List<string>> dossiers)
        {
            int indexPosition = 0;

            foreach (var dossier in dossiers)
            {
                foreach (string fullName in dossier.Value)
                {
                    Console.WriteLine($"{++indexPosition}. {dossier.Key} - {fullName}");
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
                    dossiers[position].Add(fullName);
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