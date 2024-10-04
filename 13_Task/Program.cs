namespace _13_Task
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
            float rubOnWallet = 5000;
            float usdOnWallet = 1000;
            float eurOnWallet = 2000;
            float rubToUsdCost = 65;
            float rubToEurCost = 75;
            float usdToRubCost = 0.02f;
            float usdToEurCost = 1.2f;
            float eurToRubCost = 0.01f;
            float eurToUsdCost = 0.9f;
            string userInputCommand;
            float userInputValue;
            float amountMoneyToBuy;
            bool isWork = true;
            string continueMessage = "\nНажмите любую клавишу чтобы продолжить";
            string inncorrectInputCountMessage = "У вас недостаточно средств для выполнения этой операции";
            string menuText = $"\nМеню обмена:" +
                $"\n{CommandRubToUsd} - рубль -> доллар, по курсу {rubToUsdCost} за 1 доллар" +
                $"\n{CommandRubToEur} - рубль -> евро, по курсу {rubToEurCost} за 1 евро" +
                $"\n{CommandUsdToRub} - доллар -> рубль, по курсу {usdToRubCost} за 1 рубль" +
                $"\n{CommandUsdToEur} - доллар -> евро, по курсу {usdToEurCost} за 1 евро" +
                $"\n{CommandEurToRub} - евро -> рубль, по курсу {eurToRubCost} за 1 рубль" +
                $"\n{CommandEurToUsd} - евро -> доллар, по курсу {eurToUsdCost} за 1 доллар" +
                $"\n{CommandToExit} - для выхода из пункта обмена";
            string requestCommandMessage = $"\nВведите команду: ";

            while (isWork)
            {
                Console.Clear();
                Console.WriteLine($"У вас на балансе: {rubOnWallet} рублей, {usdOnWallet} долларов, {eurOnWallet} евро");
                Console.WriteLine(menuText);
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

                        if (rubOnWallet < userInputValue)
                        {
                            Console.WriteLine(inncorrectInputCountMessage);
                            break;
                        }

                        amountMoneyToBuy = userInputValue / rubToUsdCost;
                        rubOnWallet -= userInputValue;
                        usdOnWallet += amountMoneyToBuy;
                        Console.WriteLine($"Вы обменяли {userInputValue} рублей на {amountMoneyToBuy} долларов");
                        break;

                    case CommandRubToEur:
                        Console.Write("Сколько рублей вы хотите обменять на евро? ");
                        userInputValue = Convert.ToSingle(Console.ReadLine());

                        if (rubOnWallet < userInputValue)
                        {
                            Console.WriteLine(inncorrectInputCountMessage);
                            break;
                        }

                        amountMoneyToBuy = userInputValue / rubToEurCost;
                        rubOnWallet -= userInputValue;
                        eurOnWallet += amountMoneyToBuy;
                        Console.WriteLine($"Вы обменяли {userInputValue} рублей на {amountMoneyToBuy} евро");
                        break;

                    case CommandUsdToRub:
                        Console.Write("Сколько долларов вы хотите обменять на рубли? ");
                        userInputValue = Convert.ToSingle(Console.ReadLine());

                        if (usdOnWallet < userInputValue)
                        {
                            Console.WriteLine(inncorrectInputCountMessage);
                            break;
                        }

                        amountMoneyToBuy = userInputValue / usdToRubCost;
                        usdOnWallet -= userInputValue;
                        rubOnWallet += amountMoneyToBuy;
                        Console.WriteLine($"Вы обменяли {userInputValue} долларов на {amountMoneyToBuy} рублей");
                        break;

                    case CommandUsdToEur:
                        Console.Write("Сколько долларов вы хотите обменять на евро? ");
                        userInputValue = Convert.ToSingle(Console.ReadLine());

                        if (usdOnWallet < userInputValue)
                        {
                            Console.WriteLine(inncorrectInputCountMessage);
                            break;
                        }

                        amountMoneyToBuy = userInputValue / usdToEurCost;
                        usdOnWallet -= userInputValue;
                        eurOnWallet += amountMoneyToBuy;
                        Console.WriteLine($"Вы обменяли {userInputValue} долларов на {amountMoneyToBuy} евро");
                        break;

                    case CommandEurToRub:
                        Console.Write("Сколько евро вы хотите обменять на рубли? ");
                        userInputValue = Convert.ToSingle(Console.ReadLine());

                        if (eurOnWallet < userInputValue)
                        {
                            Console.WriteLine(inncorrectInputCountMessage);
                            break;
                        }

                        amountMoneyToBuy = userInputValue / eurToRubCost;
                        eurOnWallet -= userInputValue;
                        rubOnWallet += amountMoneyToBuy;
                        Console.WriteLine($"Вы обменяли {userInputValue} евро на {amountMoneyToBuy} рублей");
                        break;

                    case CommandEurToUsd:
                        Console.Write("Сколько евро вы хотите обменять на доллары? ");
                        userInputValue = Convert.ToSingle(Console.ReadLine());

                        if (eurOnWallet < userInputValue)
                        {
                            Console.WriteLine(inncorrectInputCountMessage);
                            break;
                        }

                        amountMoneyToBuy = userInputValue / eurToRubCost;
                        eurOnWallet -= userInputValue;
                        usdOnWallet += amountMoneyToBuy;
                        Console.WriteLine($"Вы обменяли {userInputValue} евро на {amountMoneyToBuy} долларов");
                        break;

                    default:
                        Console.Write($"Такой команды не существует, попробуйте ввести другую");
                        break;
                }

                Console.WriteLine(continueMessage);
                Console.ReadLine();
            }
        }
    }
}