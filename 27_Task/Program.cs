namespace _27_Task
{
    public class Program
    {
        static void Main()
        {
            Console.Title = "ДЗ: Скобочное выражение";
            string userInput;
            int bracketsCount = 0;
            int depthCount = 0;
            int balanceValue = 0;

            char bracketOpenChar = '(';
            char bracketCloseChar = ')';

            Console.WriteLine("Введите строку из символов: \"(\" и \")\".");
            userInput = Console.ReadLine();

            for (int i = 0; i < userInput.Length; i++)
            {
                if (userInput[i] == bracketOpenChar)
                {
                    bracketsCount++;
                }
                else if (userInput[i] == bracketCloseChar)
                {
                    bracketsCount--;

                    if (bracketsCount < balanceValue)
                    {
                        break;
                    }
                }

                if (bracketsCount > depthCount)
                {
                    depthCount = bracketsCount;
                }
            }

            if (bracketsCount == balanceValue)
            {
                Console.WriteLine($"\"{userInput}\" - cтрока корректная, максимальная глубина составляет: {depthCount}");
            }
            else
            {
                Console.WriteLine($"\"{userInput}\" - некорректная строка!");
            }

            Console.ReadKey();
        }
    }
}