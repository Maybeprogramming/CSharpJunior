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
            string userInput;
            bool isRunProgramm = true;
            string continueMessage = $"\nНажмите любую клавишу чтобы продолжить";
            string commandMenu = $"Меню:" +
                                 $"\n{CommandSum} - сумма всех чисел" +
                                 $"\n{CommandExit} - выйти из приложения\n";
            int inputNumber;
            bool isNumber;

            while (isRunProgramm == true)
            {
                Console.WriteLine(commandMenu);
                Console.Write("Введенные числа: ");

                foreach (int number in numbersList)
                {
                    Console.Write($"{number} ");
                }

                Console.Write("\n\nВведите команду или число: ");
                userInput = Console.ReadLine();
                isNumber = Int32.TryParse(userInput, out int result);

                switch (userInput)
                {
                    case CommandSum:
                        SumNumbers(numbersList);
                        break;

                    case CommandExit:
                        isRunProgramm = false;
                        break;

                    default:
                        TryAddNumberToList(numbersList, isNumber, result);
                        break;
                }

                Console.WriteLine(continueMessage);
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static void TryAddNumberToList(List<int> numberList, bool isTryParseToInt, int result)
        {
            if (isTryParseToInt == true)
            {
                numberList.Add(result);
            }
            else
            {
                Console.WriteLine("Ошибка! Такого числа или команды нет!");
            }
        }

        private static void SumNumbers(List<int> numberList)
        {
            int sumNumbers = 0;

            if (numberList.Count > 0)
            {
                sumNumbers = numberList.Sum(number => number);
                Console.WriteLine($"Cумма всех введенных чисел равна: {sumNumbers}");
            }
            else
            {
                Console.WriteLine($"\nОшибка! Вы ещё не ввели ни одного числа!");
            }

            sumNumbers = 0;
        }
    }
}