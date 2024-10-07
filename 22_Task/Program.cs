namespace _22_Task
{
    public class Program
    {
        static void Main()
        {
            const string CommandSum = "sum";
            const string CommandExit = "exit";

            string titleText = "ДЗ: Динамический массив";
            int[] numbers = new int[0];
            bool isWork = true;
            string requesеMessage = $"\nВведите команду или число: ";
            string continueMessage = $"\nНажмите любую клавишу чтобы продолжить";
            string commandMenu = $"{CommandSum} - команда для вычисления суммы всех чисел в массиве" +
                                 $"\n{CommandExit} - выйти из приложения";
            string noElementsInArraymessage = $"\nОшибка! Вы ещё не ввели ни одного числа!";
            string errorCommandMessage = "Ошибка! Такого числа или команды нет!";
            string userInput;
            int inputNumber;
            int sumNumbers = 0;
            bool isParseToInt;

            Console.Title = titleText;

            while (isWork == true)
            {
                Console.WriteLine(commandMenu);
                Console.Write("Введенные числа: ");

                foreach (int number in numbers)
                {
                    Console.Write($"{number} ");
                }

                Console.WriteLine(requesеMessage);
                userInput = Console.ReadLine();
                isParseToInt = Int32.TryParse(userInput, out inputNumber);

                switch (userInput)
                {
                    case CommandSum:
                        if (numbers.Length > 0)
                        {
                            foreach (int number in numbers)
                            {
                                sumNumbers += number;
                            }

                            Console.WriteLine($"Cумма всех введенных чисел равна: {sumNumbers}");
                            sumNumbers = 0;
                        }
                        else
                        {
                            Console.WriteLine(noElementsInArraymessage);
                        }

                        break;

                    case CommandExit:
                        isWork = false;
                        break;

                    default:
                        if (isParseToInt == true)
                        {
                            int[] tempNumbers = new int[numbers.Length + 1];

                            for (int i = 0; i < numbers.Length; i++)
                            {
                                tempNumbers[i] = numbers[i];
                            }

                            tempNumbers[tempNumbers.Length - 1] = inputNumber;
                            numbers = tempNumbers;
                        }
                        else
                        {
                            Console.WriteLine(errorCommandMessage);
                        }

                        break;
                }

                Console.WriteLine(continueMessage);
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}