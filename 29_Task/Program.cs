namespace _29_Task
{
    public class Program
    {
        private static void Main()
        {
            Console.Title = "ДЗ: UIElement";

            string healthBarName = "HP";
            int healthPercentValue = 40;
            int topPositionHealthBar = 1;
            int leftPositionHealthBar = 5;
            int lengthHealthBar = 20;

            string manaBarName = "MP";
            int manaPercentValue = 60;
            int topPositionManaBar = 3;
            int leftPositionManaBar = 5;
            int lengthManaBar = 10;

            DrawBar(healthBarName, healthPercentValue, topPositionHealthBar, leftPositionHealthBar, lengthHealthBar);
            DrawBar(manaBarName, manaPercentValue, topPositionManaBar, leftPositionManaBar, lengthManaBar);

            Console.ReadLine();
        }

        private static void DrawBar(string nameBar, int currentPercent, int topPosition = 0, int leftPosition = 0, int barLength = 10)
        {
            string bar = "";
            char valueChar = '#';
            char emptyChar = '_';
            int maxPercent = 100;

            int valueForDraw = barLength * currentPercent / maxPercent;

            for (int i = 0; i < valueForDraw; i++)
            {
                bar += valueChar;
            }

            Console.SetCursorPosition(leftPosition, topPosition);
            Console.Write($"{nameBar}: [{bar}");

            bar = "";

            for (int i = valueForDraw; i < barLength; i++)
            {
                bar += emptyChar;
            }

            Console.Write($"{bar}] [{currentPercent}/{maxPercent}%]");
        }
    }
}