namespace _29_Task
{
    public class Program
    {
        private static void Main()
        {
            Console.Title = "ДЗ: UIElement";

            string healthBarName = "HP";
            int healthPercentValue = 250;
            int topPositionHealthBar = 1;
            int leftPositionHealthBar = 5;
            int lengthHealthBar = 20;

            string manaBarName = "MP";
            int manaPercentValue = 40;
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

            if (currentPercent > maxPercent)
            {
                currentPercent = maxPercent;
            }

            int valueForDraw = barLength * currentPercent / maxPercent;

            bar += FillBar(valueChar, valueForDraw);

            Console.SetCursorPosition(leftPosition, topPosition);
            Console.Write($"{nameBar}: [{bar}");

            bar = "";
            bar += FillBar(emptyChar, barLength, valueForDraw);

            Console.Write($"{bar}] [{currentPercent}/{maxPercent}%]");
        }

        private static string FillBar(char valueChar, int toValue, int fromValue = 0)
        {
            string bar = String.Empty;

            for (int i = fromValue; i < toValue; i++)
            {
                bar += valueChar;
            }

            return bar;
        }
    }
}