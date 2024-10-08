using UserUtils;

namespace _28_Task
{
    public class Program
    {
        private static void Main()
        {
            Console.Title = "ДЗ: ReadInt";

            Work();
        }

        private static void Work()
        {
            bool isTryParse = false;
            string requestMessage = "Введите число: ";
            string continueMessage = "\n\nНажмите любую клавишу чтобы продолжить";
            string userInput;
            int number;

            while (isTryParse == false)
            {
                Console.Clear();
                Display.Print(requestMessage);
                userInput = Console.ReadLine();
                isTryParse = TryParseToInt(userInput, out number);

                if (isTryParse == true)
                {
                    Display.Print($"\nВы ввели число: {number}", ConsoleColor.Green);
                }
                else
                {
                    Display.Print($"\nВы ввели не число: {userInput}", ConsoleColor.Red);
                }

                Display.Print(continueMessage);
                Console.ReadLine();
            }
        }

        private static bool TryParseToInt(string userInput, out int result)
        {
            return Int32.TryParse(userInput, out result);
        }        
    }
}