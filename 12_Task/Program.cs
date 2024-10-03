namespace _12_Task
{
    public class Program
    {
        private static void Main()
        {
            const string CommandSetName = "1";
            const string CommandSetAge = "2";
            const string CommandShowRandomNumber = "3";
            const string CommandClearConsole = "4";
            const string CommandToExit = "exit";

            Console.Title = "ДЗ: Консольное меню";
            Random random = new Random();
            int randomNumber;
            string userInput;
            string name;
            int minLengthName = 1;
            int age;
            bool isWork = true;
            string continueMessege = "\nНажмите клавишу чтобы продолжить...";
            string consoleMenu = $"Консольное меню:" +
                                  $"\n{CommandSetName} - ввести ваше имя" +
                                  $"\n{CommandSetAge} - ввести ваш возраст" +
                                  $"\n{CommandShowRandomNumber} - показать случайное число" +
                                  $"\n{CommandClearConsole} - очистить консоль" +
                                  $"\n{CommandToExit} - для выхода из программы";
            string requestCommandMessage = $"Введите команду: ";
            string inputWrongCommandMessage = "Такой команды не существует, попробуйте ввести другую команду..";

            while (isWork)
            {
                Console.WriteLine(consoleMenu);
                Console.WriteLine(requestCommandMessage);
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandToExit:
                        isWork = false;
                        break;

                    case CommandSetName:
                        Console.WriteLine("Напишите ваше имя: ");
                        name = Console.ReadLine();

                        if (name.Length >= minLengthName)
                        {
                            Console.WriteLine($"Установлено имя: {name}");
                        }
                        else
                        {
                            Console.WriteLine("Длина имени должна быть больше 1 символа");
                        }

                        Console.WriteLine(continueMessege);
                        Console.ReadLine();
                        break;

                    case CommandSetAge:
                        Console.WriteLine("Укажите ваш возраст: ");
                        age = Convert.ToInt32(Console.ReadLine());

                        if (age > 0)
                        {
                            Console.WriteLine($"Вы указали свой возраст - {age} лет");
                        }
                        else
                        {
                            Console.WriteLine($"Вы ввели некорректный возраст: \"{age}\", попробуйте снова");
                        }

                        Console.WriteLine(continueMessege);
                        Console.ReadLine();
                        break;

                    case CommandShowRandomNumber:
                        randomNumber = random.Next();
                        Console.WriteLine($"Случайное число: {randomNumber}");
                        Console.WriteLine(continueMessege);
                        Console.ReadLine();
                        break;

                    case CommandClearConsole:
                        Console.Clear();
                        break;    

                    default:
                        Console.WriteLine(inputWrongCommandMessage);
                        Console.WriteLine(continueMessege);
                        Console.ReadLine();
                        break;
                }
            }

            Console.WriteLine("\nРабота программы завершена.");
            Console.ReadKey();
        }
    }
}