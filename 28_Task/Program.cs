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
            string requestNumber = "Введите число: ";
            string continueMessage = "\n\nНажмите любую клавишу чтобы продолжить...";
            string userInput;
            int number;

            while (isTryParse == false)
            {
                Console.Clear();
                Display.Print(requestNumber);
                userInput = Console.ReadLine();
                number = ParseStringToInt(userInput, out bool isParseToInt);

                if (isParseToInt == true)
                {
                    Display.Print($"\nВы ввели число: {number}", ConsoleColor.Green);
                    isTryParse = isParseToInt;
                }
                else
                {
                    Display.Print($"\nВы ввели не число: {userInput}", ConsoleColor.Red);
                }

                Display.Print(continueMessage);
                Console.ReadLine();
            }
        }

        private static int ParseStringToInt(string userInput, out bool isParseToInt)
        {
            isParseToInt = int.TryParse(userInput, out int result);
            return result;
        }        
    }
}