namespace _09_Task
{
    public class Program
    {
        private static void Main()
        {            
            const string CommandGoToCafe = "1";
            const string CommandGoToCinema = "2";
            const string CommandGoToBank = "3";
            const string CommandGoToExit = "exit";

            Console.Title = "ДЗ: Контроль выхода";
            string userInputCommand;
            bool isTired = false;
            string WelcomeMessage = "Добро пожаловать в развлекательный центр";
            string MenuHeaderMessage = "Введите команду для перехода";
            string ContinueMessage = "\nЧтобы продолжить нажмите любую клавишу...";

            while (isTired == false)
            {
                Console.Clear();

                Console.WriteLine(WelcomeMessage);
                Console.WriteLine(MenuHeaderMessage);
                Console.WriteLine($"{CommandGoToCafe} - пройти в кафе");
                Console.WriteLine($"{CommandGoToCinema} - пройти в кинотеатр");
                Console.WriteLine($"{CommandGoToBank} - пройти к банкомату");
                Console.WriteLine($"{CommandGoToExit} - выйти");
                Console.Write("\nВаша команда: ");

                userInputCommand = Console.ReadLine().ToLower();

                switch (userInputCommand)
                {
                    case CommandGoToExit:
                        isTired = true;
                        Console.WriteLine("Вы вышли из центра");
                        break;

                    case CommandGoToCafe:
                        Console.WriteLine("Вы заказали кофе с круасаном в кафе");
                        break;

                    case CommandGoToCinema:
                        Console.WriteLine("Вы купили билет на фильм: Дедпул и Росамаха");
                        break;

                    case CommandGoToBank:
                        Console.WriteLine("Вы воспользовались банкоматом для снятия наличных");
                        break;

                    default:
                        Console.WriteLine($"Такой команды - \"{userInputCommand}\" нет, попробуйте ввести другую");
                        Console.ReadKey();
                        continue;
                }

                Console.WriteLine(ContinueMessage);
                Console.ReadKey();
            }
        }
    }
}