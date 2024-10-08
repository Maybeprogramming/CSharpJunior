namespace _30_Task
{
    public class Program
    {
        private static void Main()
        {
            Console.Title = "ДЗ: Канзас сити шафл";

            ConsoleSetup();
            Work();

            Console.ReadKey();
        }

        private static void Work()
        {
            Random random = new();

            int[] arrayNumbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

            Print($"Массив типа Int\n");
            Print("Исходный массив: ");
            Print(arrayNumbers, ConsoleColor.Green);

            arrayNumbers = Shuffle(arrayNumbers, random);

            Print("Перемешанный массив: ");
            Print(arrayNumbers, ConsoleColor.Yellow);
        }

        private static void ConsoleSetup()
        {
            int screenWidth = 100;
            int screenHeight = 30;
            int bufferWidth = screenWidth;
            int bufferHeight = 40;
            Console.SetWindowSize(screenWidth, screenHeight);
            Console.SetBufferSize(bufferWidth, bufferHeight);
        }

        private static T[] Shuffle<T>(T[] array, Random random)
        {
            int index;

            for (int i = 0; i < array.Length; i++)
            {
                index = random.Next(array.Length);
                T element = array[array.Length - i - 1];
                array[array.Length - i - 1] = array[index];
                array[index] = element;
            }

            return array;
        }

        private static void Print<T>(T[] array, ConsoleColor color = ConsoleColor.White)
        {
            foreach (T element in array)
            {
                Print($"{element} ", color);
            }

            Console.WriteLine();
        }

        private static void Print(string text, ConsoleColor color = ConsoleColor.White)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = defaultColor;
        }
    }
}