namespace _31_Task
{
    public class Program
    {
        private static void Main()
        {
            const string CommandAddDossier = "1";
            const string CommandShowDossier = "2";
            const string ComandDeleteDossier = "3";
            const string CommandFindDossier = "4";
            const string CommandExit = "5";

            Console.Title = "ДЗ: Кадровый учет";

            string[] names = { "Василий Петрович Пупкин", "Геннадий Сергеевич Иванов", "Александр Викторович Махов", "Александр Петрович Пупкин" };
            string[] positions = { "Главный инженер", "Специалист по кадрам", "Диспетчер", "Механик" };
            bool isWork = true;
            string menu = $"Меню программы по учёту кадров организации:" +
                         $"\n{CommandAddDossier} - Добавить досье сотрудника" +
                         $"\n{CommandShowDossier} - Вывести все досье на сотрудников" +
                         $"\n{ComandDeleteDossier} - Удалить досье сотрудника организации" +
                         $"\n{CommandFindDossier} - Поиск досье по фамилии сотрудника" +
                         $"\n{CommandExit} - Выйти из программы";
            string requestMessage = "\nВведите команду: ";
            string errorMessageMenu = "Такой команды нет";
            string userInput;
            string continueMessage = "\nДля продолжения нажмите любую клавишу...";

            while (isWork)
            {
                Console.Clear();
                Console.WriteLine(menu);
                Console.Write(requestMessage);
                userInput = Console.ReadLine().ToLower();

                switch (userInput)
                {
                    case CommandAddDossier:
                        AddDossier(ref names, ref positions);
                        break;

                    case CommandShowDossier:
                        ShowAllDossier(names, positions);
                        break;

                    case ComandDeleteDossier:
                        DeleteDossier(ref names, ref positions);
                        break;

                    case CommandFindDossier:
                        FindDossier(names, positions);
                        break;

                    case CommandExit:
                        isWork = false;
                        break;

                    default:
                        Console.Write($"\n{errorMessageMenu}\n");
                        break;
                }

                Console.WriteLine(continueMessage);
                Console.ReadLine();
            }
        }

        private static void AddDossier(ref string[] names, ref string[] positions)
        {
            string userInput;
            Console.Clear();
            Console.WriteLine("Добавление нового досье на сотрудника");
            Console.Write("Введите ФИО сотрудника: ");
            userInput = Console.ReadLine();
            names = AddElement(userInput, names);
            Console.Write($"Введите должность сотрудника {userInput}: ");
            userInput = Console.ReadLine();
            positions = AddElement(userInput, positions);
            Console.Write($"\nДосье успешно добавлено: {names[names.Length - 1]} - {positions[positions.Length - 1]}\n");
        }

        private static void ShowAllDossier(string[] names, string[] positions)
        {
            Console.Clear();

            if (IsEmptyDossier(names, positions) == false)
            {
                Console.Write("Архив всех досье:\n");
                ConsoleColor defaultColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;

                for (int i = 0; i < names.Length; i++)
                {
                    Console.Write($"\n{i + 1}. {names[i]} - {positions[i]}");
                }

                Console.ForegroundColor = defaultColor;
                Console.WriteLine();
            }
            else
            {
                Console.Write($"Архив с досье пуст");
            }
        }

        private static void DeleteDossier(ref string[] names, ref string[] positions)
        {
            string userInput;
            Console.Clear();
            ShowAllDossier(names, positions);

            if (IsEmptyDossier(names, positions))
            {
                return;
            }

            Console.Write($"\nДля удаления досье введите порядковый номер сотрудника из списка: ");
            userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int indexToRemove))
            {
                --indexToRemove;

                if (indexToRemove >= 0 && indexToRemove < names.Length)
                {
                    Console.Write($"\nДосье успешно удалено: {names[indexToRemove]} - {positions[indexToRemove]}\n");
                    names = RemoveElement(indexToRemove, names);
                    positions = RemoveElement(indexToRemove, positions);
                }
                else
                {
                    Console.Write($"\n[{indexToRemove + 1}] - такого индекса нет\n");
                }
            }
            else
            {
                Console.Write($"\n[{userInput}] - вы ввели не число\n");
            }
        }

        private static void FindDossier(string[] names, string[] positions)
        {
            string userInput;
            Console.Clear();
            Console.Write("Введите фамилию сотрудника для поиска: ");
            userInput = Console.ReadLine();
            int findSurnameCount = 0;
            bool isFoundEmployee = false;
            char symbolToSplit = ' ';
            Console.WriteLine($"Совпадения по запросу [{userInput}]:");

            for (int i = 0; i < names.Length; i++)
            {
                string[] surname = names[i].Split(symbolToSplit);

                for (int j = 0; j < surname.Length; j++)
                {
                    if (userInput.ToLower() == surname[j].ToLower())
                    {
                        Console.Write($"\n{findSurnameCount + 1} - {names[i]} занимает должность: {positions[i]}\n");
                        findSurnameCount++;
                        isFoundEmployee = true;
                    }
                }
            }

            if (isFoundEmployee == false)
            {
                Console.Write($"- Не найдены!\n");
            }
        }

        private static string[] AddElement(string inputString, string[] sourceArray)
        {
            string[] tempArray = new string[sourceArray.Length + 1];

            for (int i = 0; i < sourceArray.Length; i++)
            {
                tempArray[i] = sourceArray[i];
            }

            tempArray[tempArray.Length - 1] = inputString;
            return tempArray;
        }

        private static string[] RemoveElement(int indexToRemove, string[] sourceArray)
        {
            string[] tempArray = new string[sourceArray.Length - 1];

            for (int i = 0; i < indexToRemove; i++)
            {
                tempArray[i] = sourceArray[i];
            }

            for (int i = indexToRemove + 1; i < sourceArray.Length; i++)
            {
                tempArray[i - 1] = sourceArray[i];
            }

            return tempArray;
        }

        private static bool IsEmptyDossier(string[] names, string[] positions)
        {
            int emptyValue = 0;

            return names.Length == emptyValue && positions.Length == emptyValue;
        }
    }
}