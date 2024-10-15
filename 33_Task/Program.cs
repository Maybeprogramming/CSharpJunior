namespace _33_Task
{
    public class Program
    {
        private static void Main()
        {
            Console.Title = "ДЗ: Толковый словарь";
            StartWorkDictionary();
        }

        private static void StartWorkDictionary()
        {
            const string FindWordCommand = "1";
            const string PrintWorldsCommand = "2";
            const string Exit = "3";

            Dictionary<string, string> dictionaryWords = InitialDictionary(); ;
            string titleMenu = "Меню \"толкового словаря\":";
            string menu = $"\n{FindWordCommand} - Найти значение слова" +
                          $"\n{PrintWorldsCommand} - Показать список слов - значений" +
                          $"\n{Exit} - Выход из программы";
            string requestMessage = "\nВведите команду: ";
            string userInput;
            string continueMessage = "\nНажмите любую клавишу чтобы продолжить...";
            string noSuchCommandMessage = "Такой команды нет!";
            bool isRun = true;

            while (isRun)
            {
                Console.Clear();
                Print(titleMenu);
                Print(menu);
                Print(requestMessage);
                userInput = Console.ReadLine();
                Console.WriteLine();

                switch (userInput)
                {
                    case FindWordCommand:
                        TryFindWord(dictionaryWords);
                        break;

                    case PrintWorldsCommand:
                        PrintAllWords(dictionaryWords);
                        break;

                    case Exit:
                        isRun = false;
                        break;

                    default:
                        Print($"\"{userInput}\" - {noSuchCommandMessage}");
                        break;
                }

                Print(continueMessage);
                Console.ReadLine();
            }
        }

        private static void TryFindWord(Dictionary<string, string> dictionary)
        {
            string noSuchWordMessage = "Такого слова нет в данном словаре, попробуйте снова";
            string requestWordMessage = "\nВведите слово: ";
            string userInput;

            Print(requestWordMessage);
            userInput = Console.ReadLine();

            if (dictionary.ContainsKey(userInput.ToUpperInvariant()))
            {
                Print($"\n{userInput} - {dictionary[userInput]}\n");
            }
            else
            {
                Print($"\n[{userInput}] - {noSuchWordMessage}\n");
            }
        }

        private static void Print(string text, ConsoleColor consoleColor = ConsoleColor.DarkYellow)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = consoleColor;
            Console.Write(text);
            Console.ForegroundColor = defaultColor;
        }

        private static void PrintAllWords(Dictionary<string, string> dictionaryWords)
        {
            int indexElement = 1;
            Print("Перечень слов и их значений: ");

            foreach (var word in dictionaryWords)
            {
                Print($"\n{indexElement}. [{word.Key}] - {word.Value}");
                indexElement++;
            }
        }

        private static Dictionary<string, string> InitialDictionary()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>()
            {
                {"программист", "Специалист по программированию" },
                {"Комрьютер", "Электронная вычислительная машина (ЭВМ). Персональный компьютер" },
                {"слово", "Единица языка, служащая для наименования понятий, предметов, лиц, действий, состояний, признаков, связей, отношений, оценок" },
                {"бит", "Единица измерения количества информации" },
                {"кремний", "Химический элемент, тёмно-серые кристаллы с металлическим блеском, одна из главных составных частей горных пород" },
                {"лазер", "Оптический квантовый генератор, устройство для получения мощных узконаправленных пучков света" },
                {"логика", "Ход рассуждений, умозаключений. Разумность, внутренняя закономерность вещей, событий" },
                {"aлгоритм","Совокупность действий, правил для решения данной задачи" },
                {"aтом","Мельчайшая частица химического элемента, состоящая из ядра и электронов" },
                {"cила","Величина, являющаяся мерой механического взаимодействия тел, вызывающего их ускорение или деформацию; характеристика интенсивности физических процессов" },
            };

            return dictionary;
        }
    }
}