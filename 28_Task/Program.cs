namespace _28_Task
{
    public class Program
    {
        private static void Main()
        {
            Console.Title = "ДЗ: ReadInt";

            int number = TryParseToInt();
            Console.WriteLine($"Вы ввели число: {number}");
        }

        private static int TryParseToInt()
        {
            bool isTryParse = false;
            string requestNumber = "Введите число: ";
            string userInput;
            int result = 0;

            while (isTryParse == false)
            {
                Console.Clear();
                Console.WriteLine(requestNumber);
                userInput = Console.ReadLine();
                isTryParse = int.TryParse(userInput, out result);

                if (isTryParse == false)
                {
                    Console.WriteLine($"Вы ввели не число: {userInput}", ConsoleColor.Red);
                    Console.ReadLine();
                }
            }

            return result;
        }
    }
}