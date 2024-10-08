using UserUtils;

namespace _29_Task
{
    public class Program
    {
        private static void Main()
        {
            const string TakeDamageCommand = "1";
            const string GiveHealingCommand = "2";
            const string TakeManaCommand = "3";
            const string GiveManaCommand = "4";
            const string SetPositionBarsCommand = "5";
            const string ExitCommand = "6";

            int currentHealth = 4;
            int maxHealth = 10;
            string nameHealthBar = "HP";
            int leftPositionHealth = 0;
            int topPositionHealth = 0;
            int leftPositionMana = 0;
            int topPositionMana = 1;
            int currentMana = 6;
            int maxMana = 10;
            string nameManaBar = "MP";
            int leftPositionMenu = 0;
            int topPositionMenu = 3;
            bool isWork = true;
            string userInput;
            string continueMessage = "\nДля продолжения нажмите любую клавишу...";
            string errorMessageMenu = "Такой команды нет";
            string menu = $"Выберите действие:" +
                          $"\n{TakeDamageCommand} - для нанесения урона" +
                          $"\n{GiveHealingCommand} - для восполнения здоровья" +
                          $"\n{TakeManaCommand} - для сжигания маны" +
                          $"\n{GiveManaCommand} - для восстановления маны" +
                          $"\n{SetPositionBarsCommand} - для установки позиции баров здоровья и маны" +
                          $"\n{ExitCommand} - для выхода из программы";
            string requestMessage = "\nВведите команду: ";
            string requestMessageDamage = "Введите сколько вы нанесёте урона";
            string requestMessageHealing = "Введите количество исцеляемого здоровья";
            string requestMessageBurnMana = "Введите сколько вы сожгёте маны";
            string requestMessageGiveMana = "Введите количество маны для восполнения";
            string lowHealthMessage = "У цели нет здоровья";
            string lowManaMessage = "У цели нет маны";
            string maxHealthMessage = "Цель не нуждается в лечении";
            string maxManaMessage = "Цель не нуждается в пополнении маны";
            string exitMessage = "Выход из программы";

            while (isWork == true)
            {
                Console.Clear();
                DrawBar(nameHealthBar, currentHealth, maxHealth, ConsoleColor.Red, topPositionHealth, leftPositionHealth);
                DrawBar(nameManaBar, currentMana, maxMana, ConsoleColor.Blue, topPositionMana, leftPositionMana);
                Display.Print("\n");
                DrawMenu(menu, leftPositionMenu, topPositionMenu);
                Display.Print(requestMessage);
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case TakeDamageCommand:
                        currentHealth -= TakeParametrValue(currentHealth, requestMessageDamage, lowHealthMessage);
                        break;

                    case GiveHealingCommand:
                        currentHealth += GiveParametrValue(currentHealth, maxHealth, requestMessageHealing, maxHealthMessage);
                        break;

                    case TakeManaCommand:
                        currentMana -= TakeParametrValue(currentMana, requestMessageBurnMana, lowManaMessage);
                        break;

                    case GiveManaCommand:
                        currentMana += GiveParametrValue(currentMana, maxMana, requestMessageGiveMana, maxManaMessage);
                        break;

                    case SetPositionBarsCommand:
                        SetPositionUserInterface(ref leftPositionHealth, ref topPositionHealth, ref leftPositionMana, ref topPositionMana, ref leftPositionMenu, ref topPositionMenu);
                        break;

                    case ExitCommand:
                        isWork = false;
                        break;

                    default:
                        Display.Print($"\n\"{userInput}\" - {errorMessageMenu}\n", ConsoleColor.Red);
                        break;
                }

                Display.Print(continueMessage);
                Console.ReadLine();
            }

            Display.Print("\n" + exitMessage);
            Console.ReadLine();
        }

        private static void DrawMenu(string menuText, int leftPositionMenu, int topPositionMenu)
        {
            Console.SetCursorPosition(leftPositionMenu, topPositionMenu);
            Display.Print(menuText);
        }

        private static void DrawBar(string nameBar, int value, int maxValue, ConsoleColor color, int topPosition, int leftPosition, int barLength = 10, char valueSymbol = '#', char remainingSymbol = '_')
        {
            ConsoleColor defaultColor = Console.BackgroundColor;
            string bar = "";
            int fullScalePercent = 100;
            int maxDrawSymbols = 5;
            int valueForDraw = value * fullScalePercent / maxValue / maxDrawSymbols;
            float remainderOfDivision = ((float)value * fullScalePercent) / maxValue - (valueForDraw * maxDrawSymbols);

            if (remainderOfDivision > 0)
            {
                valueForDraw++;
            }

            for (int i = 0; i < valueForDraw; i++)
            {
                bar += valueSymbol;
            }

            Console.SetCursorPosition(leftPosition, topPosition);
            Console.Write('[');
            Console.BackgroundColor = color;
            Console.Write(bar);
            Console.BackgroundColor = defaultColor;

            bar = "";

            for (int i = valueForDraw; i < maxDrawSymbols; i++)
            {
                bar += remainingSymbol;
            }

            Console.Write($"{bar}] [{value}/{maxValue}] {nameBar}");
        }

        private static int TakeParametrValue(int currentStat, string requestText, string warningText)
        {
            Display.Print($"{requestText}: ");
            int userInput = Convert.ToInt32(Console.ReadLine());
            int minTakeValue = 0;

            if (currentStat <= 0)
            {
                Display.Print($"{warningText} ({currentStat})", ConsoleColor.Red);
                return minTakeValue;
            }

            if (currentStat >= userInput)
            {
                return userInput;
            }
            else
            {
                userInput = currentStat;
                return userInput;
            }
        }

        private static int GiveParametrValue(int currentStat, int maxStat, string requestText, string warningText)
        {
            Console.Write($"{requestText}: ");
            int userInput = Convert.ToInt32(Console.ReadLine());
            int remainingValue = maxStat - currentStat;
            int minGiveValue = 0;

            if (currentStat >= maxStat)
            {
                Display.Print($"{warningText} ({currentStat})", ConsoleColor.Green);
                return minGiveValue;
            }

            if (userInput <= remainingValue)
            {
                return userInput;
            }
            else
            {
                userInput = remainingValue;
                return userInput;
            }
        }

        private static void SetPositionUserInterface(ref int leftPositionHealth, ref int topPositionHealth, ref int leftPositionMana, ref int topPositionMana, ref int leftPositionMenu, ref int topPositionMenu)
        {
            int userInput;
            int marginTopPosition = 1;
            int topMenuSize = 15;
            int leftDefailtPosition = 0;
            int topDefailtSize = 0;
            Console.WriteLine("Изменение позиции бара здоровья и маны");
            Console.Write("Введите позицию по столбцу: ");
            userInput = Convert.ToInt32(Console.ReadLine());
            leftPositionHealth = userInput;
            Console.Write("Введите позицию по строке: ");
            userInput = Convert.ToInt32(Console.ReadLine());
            topPositionHealth = userInput;
            leftPositionMana = leftPositionHealth;
            topPositionMana = topPositionHealth + marginTopPosition;

            if (topPositionHealth >= topMenuSize)
            {
                leftPositionMenu = leftDefailtPosition;
                topPositionMenu = topDefailtSize;
            }
            else
            {
                leftPositionMenu = leftPositionMana + marginTopPosition;
                topPositionMenu = topPositionMana + marginTopPosition;
            }
        }
    }
}