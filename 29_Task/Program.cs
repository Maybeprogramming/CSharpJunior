namespace _29_Task
{
    public class Program
    {
        private static void Main()
        {

        }

        private static void DrawBar(string nameBar, int value, int maxValue, int topPosition, int leftPosition, int barLength = 10, char valueSymbol = '#', char remainingSymbol = '_')
        {
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
            Console.Write($"[{bar}");

            bar = "";

            for (int i = valueForDraw; i < maxDrawSymbols; i++)
            {
                bar += remainingSymbol;
            }

            Console.Write($"{bar}] [{value}/{maxValue}] {nameBar}");
        }
    }
}