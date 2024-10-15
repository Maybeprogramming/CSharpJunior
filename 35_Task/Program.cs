namespace _35_Task
{
    public class Program
    {
        private static void Main()
        {
            WorkProgramm();
        }

        private static void WorkProgramm()
        {
            const string CommandSum = "sum";
            const string CommandExit = "exit";

            Console.Title = "ДЗ: Динамический массив продвинутый";
            List<int> numbersList = new List<int>();
            int sumNumbers = 0;
            string userInput;
            bool isRunProgramm = true;
            string requestCommandOrNumber = $"\n\nВведите команду или число: ";
            string continueMessage = $"\nНажмите любую клавишу чтобы продолжить";
            string commandMenu = $"Меню:" +
                                 $"\n{CommandSum} - сумма всех чисел" +
                                 $"\n{CommandExit} - выйти из приложения\n";
            string noElementsInArraymessage = $"\nОшибка! Вы ещё не ввели ни одного числа!";
            string errorCommandMessage = "Ошибка! Такого числа или команды нет!";
            int inputNumber;
            bool isTryParseToInt;

            while (isRunProgramm == true)
            {
                Console.WriteLine(commandMenu);
                Console.Write("Введенные числа: ");

                foreach (int number in numbersList)
                {
                    Console.Write($"{number} ");
                }

                Console.Write(requestCommandOrNumber);
                userInput = Console.ReadLine();
                isTryParseToInt = Int32.TryParse(userInput, out int result);

                switch (userInput)
                {
                    case CommandSum:
                        SumNumbers(numbersList, sumNumbers, noElementsInArraymessage);
                        break;

                    case CommandExit:
                        isRunProgramm = false;
                        break;

                    default:
                        TryAddNumberToList(numbersList, errorCommandMessage, isTryParseToInt, result);
                        break;
                }

                Console.WriteLine(continueMessage);
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static void TryAddNumberToList(List<int> numberList, string errorCommandMessage, bool isTryParseToInt, int result)
        {
            if (isTryParseToInt == true)
            {
                numberList.Add(result);
            }
            else
            {
                Console.WriteLine(errorCommandMessage);
            }
        }

        private static void SumNumbers(List<int> numberList, int sumNumbers, string noElementsInArraymessage)
        {
            if (numberList.Count > 0)
            {
                sumNumbers = numberList.Sum(number => number);
                Console.WriteLine($"Cумма всех введенных чисел равна: {sumNumbers}");
            }
            else
            {
                Console.WriteLine(noElementsInArraymessage);
            }

            sumNumbers = 0;
        }
    }
}