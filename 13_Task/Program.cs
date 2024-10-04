﻿namespace _13_Task
{
    public class Program
    {
        private static void Main()
        {
            const string CommandRubToUsd = "1";
            const string CommandRubToEur = "2";
            const string CommandUsdToRub = "3";
            const string CommandUsdToEur = "4";
            const string CommandEurToRub = "5";
            const string CommandEurToUsd = "6";
            const string CommandToExit = "7";

            Console.Title = "ДЗ: Конвертер валют";
            float rubValue;
            float usdValue;
            float eurValue;
            int minRubCount = 300;
            int maxRubCount = 600;
            int minUsdCount = 20;
            int maxUsdCount = 50;
            int minEurCount = 10;
            int maxEurCount = 30;
            Random random = new Random();
            rubValue = random.Next(minRubCount, ++maxRubCount);
            usdValue = random.Next(minUsdCount, ++maxUsdCount);
            eurValue = random.Next(minEurCount, ++maxEurCount);
            float rubToUsdCost = 65;
            float rubToEurCost = 75;
            float usdToRubCost = 0.02f;
            float usdToEurCost = 1.2f;
            float eurToRubCost = 0.01f;
            float eurToUsdCost = 0.9f;
            string userInputCommand;
            float userInputValue;
            float convertMoneyCount;
            bool isWork = true;
            string continueMessage = "\nНажмите любую клавишу чтобы продолжить...";
            string welcomeMessage = "Добро пожаловать в пункт обмена валют \"Мультиконвертер\"";
            string inncorrectInputCountMessage = "У вас недостаточно средств для выполнения этой операции";
            string userBalanceInfo = $"\nУ вас на балансе: {rubValue} рублей, {usdValue} долларов, {eurValue} евро";
            string programmMenu = $"\nМеню для обмена:" +
                $"\n{CommandRubToUsd} - для обмена рублей в доллары, по курсу {rubToUsdCost} за 1 доллар" +
                $"\n{CommandRubToEur} - для обмена рублей в евро, по курсу {rubToEurCost} за 1 евро" +
                $"\n{CommandUsdToRub} - для обмена долларов в рубли, по курсу {usdToRubCost} за 1 рубль" +
                $"\n{CommandUsdToEur} - для обмена долларов в евро, по курсу {usdToEurCost} за 1 евро" +
                $"\n{CommandEurToRub} - для обмена евро в рубли, по курсу {eurToRubCost} за 1 рубль" +
                $"\n{CommandEurToUsd} - для обмена евро в доллары, по курсу {eurToUsdCost} за 1 доллар" +
                $"\n{CommandToExit} - для выхода из пункта обмена";
            string requestCommandMessage = $"\nВведите команду: ";

            while (isWork)
            {
                Console.Clear();
                Console.WriteLine(welcomeMessage);
                Console.WriteLine(userBalanceInfo);
                Console.WriteLine(programmMenu);
                Console.Write(requestCommandMessage);
                userInputCommand = Console.ReadLine();

                switch (userInputCommand)
                {
                    case CommandToExit:
                        isWork = false;
                        break;

                    case CommandRubToUsd:
                        Console.Write("Сколько рублей вы хотите обменять на доллары? ");
                        userInputValue = Convert.ToSingle(Console.ReadLine());

                        if (rubValue >= userInputValue)
                        {
                            rubValue -= userInputValue;
                            convertMoneyCount = userInputValue / rubToUsdCost;
                            usdValue += convertMoneyCount;
                            Console.WriteLine($"Вы обменяли {userInputValue} рублей на {convertMoneyCount} долларов");
                        }
                        else
                        {
                            Console.WriteLine(inncorrectInputCountMessage);
                        }

                        Console.WriteLine(continueMessage);
                        Console.ReadLine();
                        break;

                    case CommandRubToEur:
                        Console.Write("Сколько рублей вы хотите обменять на евро? ");
                        userInputValue = Convert.ToSingle(Console.ReadLine());

                        if (rubValue >= userInputValue)
                        {
                            rubValue -= userInputValue;
                            convertMoneyCount = userInputValue / rubToEurCost;
                            eurValue += convertMoneyCount;
                            Console.WriteLine($"Вы обменяли {userInputValue} рублей на {convertMoneyCount} евро");
                        }
                        else
                        {
                            Console.WriteLine(inncorrectInputCountMessage);
                        }

                        Console.WriteLine(continueMessage);
                        Console.ReadLine();
                        break;

                    case CommandUsdToRub:
                        Console.Write("Сколько долларов вы хотите обменять на рубли? ");
                        userInputValue = Convert.ToSingle(Console.ReadLine());

                        if (usdValue >= userInputValue)
                        {
                            usdValue -= userInputValue;
                            convertMoneyCount = userInputValue / usdToRubCost;
                            rubValue += convertMoneyCount;
                            Console.WriteLine($"Вы обменяли {userInputValue} долларов на {convertMoneyCount} рублей");
                        }
                        else
                        {
                            Console.WriteLine(inncorrectInputCountMessage);
                        }

                        Console.WriteLine(continueMessage);
                        Console.ReadLine();
                        break;

                    case CommandUsdToEur:
                        Console.Write("Сколько долларов вы хотите обменять на евро? ");
                        userInputValue = Convert.ToSingle(Console.ReadLine());

                        if (usdValue >= userInputValue)
                        {
                            usdValue -= userInputValue;
                            convertMoneyCount = userInputValue / usdToEurCost;
                            eurValue += convertMoneyCount;
                            Console.WriteLine($"Вы обменяли {userInputValue} долларов на {convertMoneyCount} евро");
                        }
                        else
                        {
                            Console.WriteLine(inncorrectInputCountMessage);
                        }

                        Console.WriteLine(continueMessage);
                        Console.ReadLine();
                        break;

                    case CommandEurToRub:
                        Console.Write("Сколько евро вы хотите обменять на рубли? ");
                        userInputValue = Convert.ToSingle(Console.ReadLine());

                        if (eurValue >= userInputValue)
                        {
                            eurValue -= userInputValue;
                            convertMoneyCount = userInputValue / eurToRubCost;
                            rubValue += convertMoneyCount;
                            Console.WriteLine($"Вы обменяли {userInputValue} евро на {convertMoneyCount} рублей");
                        }
                        else
                        {
                            Console.WriteLine(inncorrectInputCountMessage);
                        }

                        Console.WriteLine(continueMessage);
                        Console.ReadLine();
                        break;

                    case CommandEurToUsd:
                        Console.Write("Сколько евро вы хотите обменять на доллары? ");
                        userInputValue = Convert.ToSingle(Console.ReadLine());

                        if (eurValue >= userInputValue)
                        {
                            eurValue -= userInputValue;
                            convertMoneyCount = userInputValue / eurToRubCost;
                            usdValue += convertMoneyCount;
                            Console.WriteLine($"Вы обменяли {userInputValue} евро на {convertMoneyCount} долларов");
                        }
                        else
                        {
                            Console.WriteLine(inncorrectInputCountMessage);
                        }

                        Console.WriteLine(continueMessage);
                        Console.ReadLine();
                        break;

                    default:
                        Console.Write($"Такой команды не существует, попробуйте ввести другую");
                        Console.WriteLine(continueMessage);
                        Console.ReadLine();
                        break;
                }
            }

            Console.ReadKey();
        }
    }
}