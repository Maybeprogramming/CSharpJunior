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
            const string AddCardMenu = "1";
            const string ShowCardMenu = "2";
            const string DeleteCardMenu = "3";
            const string ExitMenu = "4";            

            Dictionary<string, string> cardsEmployees = new()
            {
                { "Василий Петрович Пупкин", "Главный инженер" },
                { "Геннадий Сергеевич Иванов", "Специалист по кадрам" },
                { "Александр Викторович Махов", "Диспетчер" },
                { "Александр Петрович Пупкин", "Механик" },
                { "Воробьев Анатолий Сергеевич", "Техник по учёту" },
            };

            bool isRunProgramm = true;
            string menu = $"Меню:" +
                         $"\n{AddCardMenu} - Добавить досье сотрудника" +
                         $"\n{ShowCardMenu} - Вывести все досье" +
                         $"\n{DeleteCardMenu} - Удалить досье сотрудника" +
                         $"\n{ExitMenu} - Выйти из программы\n";
            string requestMessage = "\nВведите команду: ";
            string errorMessageMenu = "Такой команды нет";
            string userInput;
            string continueMessage = "\nДля продолжения нажмите любую клавишу";

            while (isRunProgramm == true)
            {
                Console.Clear();
                Console.Write(menu);
                Console.Write(requestMessage);
                userInput = Console.ReadLine().ToLower();

                switch (userInput)
                {
                    case AddCardMenu:
                        TryCreateCard(cardsEmployees, continueMessage);
                        break;

                    case ShowCardMenu:
                        ShowCards(cardsEmployees);
                        break;

                    case DeleteCardMenu:
                        TryDeleteCard(cardsEmployees);
                        break;

                    case ExitMenu:
                        isRunProgramm = false;
                        break;

                    default:
                        Print($"\n[{userInput}] - {errorMessageMenu}\n", ConsoleColor.Red);
                        break;
                }

                Print(continueMessage);
                Console.ReadLine();
            }
        }

        private static void TryCreateCard(Dictionary<string, string> cardsEmployees, string continueMessage)
        {
            string inputNameKey;
            string inputRankValue;

            Console.Clear();
            Console.WriteLine("Добавление нового досье на сотрудника");

            Console.Write("Введите ФИО: ");
            inputNameKey = Console.ReadLine();

            if (cardsEmployees.ContainsKey(inputNameKey) == true)
            {
                Print($"{inputNameKey} - не допустимое значение, в базе уже есть такое досье");
                return;
            }

            Console.Write($"Введите должность {inputNameKey}: ");
            inputRankValue = Console.ReadLine();

            cardsEmployees.Add(inputNameKey, inputRankValue);
            Print($"\nДосье успешно добавлено: {inputNameKey} - {inputRankValue}\n", ConsoleColor.Green);
        }

        private static void ShowCards(Dictionary<string, string> cardsEmployeess)
        {
            Console.Clear();
            int indexPosition = 0;

            if (IsEmptyCard(cardsEmployeess) == false)
            {
                Print("Архив всех досье:\n", ConsoleColor.Green);

                foreach (var card in cardsEmployeess)
                {
                    indexPosition++;
                    Console.Write($"\n{indexPosition}. {card.Key} - {card.Value}");
                }

                Console.WriteLine();
            }
            else
            {
                Print($"Архив с досье пуст", ConsoleColor.Yellow);
            }
        }

        private static void TryDeleteCard(Dictionary<string, string> cardsEmployeess)
        {
            string userInput;
            Console.Clear();
            ShowCards(cardsEmployeess);

            if (IsEmptyCard(cardsEmployeess) == true)
                return;

            Console.Write($"\nДля удаления введите ФИО сотрудника: ");
            userInput = Console.ReadLine();

            if (cardsEmployeess.ContainsKey(userInput) == true)
            {
                cardsEmployeess.Remove(userInput);
                Print($"\nДосье успешно удалено: {userInput}\n", ConsoleColor.Green);
            }
            else
            {
                Print($"\n[{userInput}] - такого досье нет\n", ConsoleColor.Red);
            }
        }

        private static void Print(string text, ConsoleColor color = ConsoleColor.White)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = defaultColor;
        }

        private static bool IsEmptyCard(Dictionary<string, string> cardsEmployeess) => cardsEmployeess.Count <= 0;
    }
}