namespace _09_Task
{
    public class Program
    {
        private static void Main()
        {            
            const string CommandGoToExit = "exit";

            Console.Title = "ДЗ: Контроль выхода";
            string userInputCommand;
            bool isTired = false;
            string continueMessage = "\nЧтобы продолжить нажмите любую клавишу...";

            while (isTired == false)
            {
                Console.Clear();

                Console.WriteLine($"{CommandGoToExit} - выйти");
                Console.Write("\nВаша команда: ");

                userInputCommand = Console.ReadLine().ToLower();

                switch (userInputCommand)
                {
                    case CommandGoToExit:
                        isTired = true;
                        Console.WriteLine("Вы вышли...");
                        break;

                    default:
                        Console.WriteLine($"Такой команды - \"{userInputCommand}\" нет, попробуйте ввести другую");
                        Console.ReadKey();
                        continue;
                }

                Console.WriteLine(continueMessage);
                Console.ReadKey();
            }
        }
    }
}