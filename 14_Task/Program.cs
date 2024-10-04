namespace _14_Task
{
    public class Program
    {
        private static void Main()
        {
            const string CommandPrint = "1";
            const string CommandExit = "2";

            Console.Title = "ДЗ: Вывод имени";
            string userInput;
            char borderChar;
            string middleString;
            string borderString;
            string totalString;
            string nextLine = "\n";
            bool isWork = true;
            string inputCommand;
            string continueMessage = "Нажмите клавишу чтобы продолжить...";
            string programmMenu = $"Меню: " +
                                  $"\n{CommandPrint} - ввести имя и символа для рамки" +
                                  $"\n{CommandExit} - выход";
            string requestCommandMessage = $"Введите команду: ";
            string requestInputStringMessage = $"Введите строку для принта: ";
            string requestInputCharMessage = $"Введите 1 символ для рамки: ";

            while (isWork)
            {
                Console.Clear();
                Console.WriteLine(programmMenu);
                Console.Write(requestCommandMessage);
                inputCommand = Console.ReadLine();

                switch (inputCommand)
                {
                    case CommandPrint:
                        Console.WriteLine(requestInputStringMessage);
                        userInput = Console.ReadLine();

                        Console.WriteLine(requestInputCharMessage);
                        borderChar = Convert.ToChar(Console.ReadLine());

                        middleString = borderChar + userInput + borderChar;
                        borderString = new string(borderChar, middleString.Length);
                        totalString = borderString + nextLine + middleString + nextLine + borderString;
                        Console.WriteLine(totalString);
                        break;

                    case CommandExit:
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Такой команды не существует, попробуйте ввести другую");
                        break;
                }

                Console.WriteLine(continueMessage);
                Console.ReadLine();
            }
        }
    }
}