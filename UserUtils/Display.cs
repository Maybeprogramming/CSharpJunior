namespace UserUtils
{
    public static class Display
    {
        public static void Print(string text, ConsoleColor color = ConsoleColor.White)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = defaultColor;
        }
    }
}