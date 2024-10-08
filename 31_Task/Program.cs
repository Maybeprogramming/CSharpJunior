namespace _31_Task
{
    public class Program
    {
        private static void Main()
        {
            const string CreateCardMenu = "1";
            const string ShowCardMenu = "2";
            const string DeleteCardMenu = "3";
            const string FindBySernameMenu = "4";
            const string ExitMenu = "5";

            Console.Title = "ДЗ: Кадровый учет";

            string[] names = { "Василий Петрович Пупкин", "Геннадий Сергеевич Иванов", "Александр Викторович Махов", "Александр Петрович Пупкин" };
            string[] positions = { "Главный инженер", "Специалист по кадрам", "Диспетчер", "Механик" };
            bool isWork = true;
            string menu = $"Меню программы по учёту кадров организации:" +
                         $"\n{CreateCardMenu} - Добавить досье сотрудника" +
                         $"\n{ShowCardMenu} - Вывести все досье на сотрудников" +
                         $"\n{DeleteCardMenu} - Удалить досье сотрудника организации" +
                         $"\n{FindBySernameMenu} - Поиск досье по фамилии сотрудника" +
                         $"\n{ExitMenu} - Выйти из программы";
            string requestMessage = "\nВведите команду: ";
            string exitMessage = "\nРабота программы завершена\n";
            string errorMessageMenu = "Такой команды нет";
            string userInput;
            string continueMessage = "\nДля продолжения нажмите любую клавишу...";            

            while (isWork == true)
            {
                Console.Clear();
                Console.WriteLine(menu);
                Console.Write(requestMessage);
                userInput = Console.ReadLine().ToLower();

                switch (userInput)
                {
                    case CreateCardMenu:
                        CreateCard(ref names, ref positions);
                        break;

                    case ShowCardMenu:
                        ShowAllCards(names, positions);
                        break;

                    case DeleteCardMenu:
                        DeleteCard(ref names, ref positions);
                        break;

                    case FindBySernameMenu:
                        SearchBySername(names, positions);
                        break;

                    case ExitMenu:
                        isWork = false;
                        break;

                    default:
                        Console.Write($"\n\"{userInput}\" - {errorMessageMenu}\n");
                        break;
                }

                Console.WriteLine(continueMessage);
                Console.ReadLine();
            }

            Console.Write(exitMessage, ConsoleColor.Green);
        }

        private static void CreateCard(ref string[] names, ref string[] positions)
        {
            string userInput;
            Console.Clear();
            Console.WriteLine("Добавление нового досье на сотрудника");
            Console.Write("Введите ФИО сотрудника: ");
            userInput = Console.ReadLine();
            names = AddElementToArray(userInput, names);
            Console.Write($"Введите должность сотрудника {userInput}: ");
            userInput = Console.ReadLine();
            positions = AddElementToArray(userInput, positions);
            Console.Write($"\nДосье успешно добавлено: {names[names.Length - 1]} - {positions[positions.Length - 1]}\n");
        }

        private static void ShowAllCards(string[] names, string[] positions)
        {
            Console.Clear();

            if (IsEmptyCard(names, positions) == false)
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

        private static void DeleteCard(ref string[] names, ref string[] positions)
        {
            string userInput;
            Console.Clear();
            ShowAllCards(names, positions);

            if (IsEmptyCard(names, positions) == true)
            {
                return;
            }

            Console.Write($"\nДля удаления досье введите порядковый номер сотрудника из списка: ");
            userInput = Console.ReadLine();

            if (Int32.TryParse(userInput, out int indexToRemove) == true)
            {
                --indexToRemove;

                if (indexToRemove >= 0 && indexToRemove < names.Length)
                {
                    Console.Write($"\nДосье успешно удалено: {names[indexToRemove]} - {positions[indexToRemove]}\n");
                    names = RemoveElementFromArray(indexToRemove, names);
                    positions = RemoveElementFromArray(indexToRemove, positions);
                }
                else
                {
                    Console.Write($"\n\"{indexToRemove + 1}\" - такого индекса нет\n");
                }
            }
            else
            {
                Console.Write($"\n\"{userInput}\" - вы ввели не число\n");
            }
        }

        private static void SearchBySername(string[] names, string[] positions)
        {
            string userInput;
            Console.Clear();
            Console.Write("Введите фамилию сотрудника для поиска: ");
            userInput = Console.ReadLine();
            int findSurnameCount = 0;
            bool isFoundEmployee = false;
            char symbolToSplit = ' ';
            Console.WriteLine($"Совпадения по запросу \"{userInput}\":");

            for (int i = 0; i < names.Length; i++)
            {
                string[] surname = names[i].Split(symbolToSplit);

                for (int j = 0; j < surname.Length; j++)
                {
                    if (userInput.ToLower() == surname[j].ToLower())
                    {
                        Console.Write($"\n{findSurnameCount + 1} - ");
                        Console.Write($"{names[i]}");
                        Console.Write($" занимает должность: ");
                        Console.Write($"{positions[i]}\n");
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

        private static string[] AddElementToArray(string inputString, string[] sourceArray)
        {
            string[] tempArray = new string[sourceArray.Length + 1];

            for (int i = 0; i < sourceArray.Length; i++)
            {
                tempArray[i] = sourceArray[i];
            }

            tempArray[tempArray.Length - 1] = inputString;
            sourceArray = tempArray;
            return sourceArray;
        }

        private static string[] RemoveElementFromArray(int indexToRemove, string[] sourceArray)
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

            sourceArray = tempArray;
            return sourceArray;
        }

        private static bool IsEmptyCard(string[] names, string[] positions)
        {
            int emptyValue = 0;

            if (names.Length == emptyValue && positions.Length == emptyValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}