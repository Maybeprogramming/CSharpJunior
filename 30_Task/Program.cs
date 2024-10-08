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
            char[] arraySymbols = { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')' };
            string[] arrayText = { "Один", "Два", "Три", "Четыре", "Пять" };
            bool[] arrarBool = { true, true, true, false, false, false };
            float[] arrayFloat = { 1.1f, 2.2f, 3.3f, 4.4f, 5.5f, 6.6f, 7.7f, 8.8f, 9.9f, 0.01f };

            StartShuffle(random, arrayNumbers, "Массив типа Int");
            StartShuffle(random, arraySymbols, "Массив типа Char");
            StartShuffle(random, arrayText, "Массив типа String");
            StartShuffle(random, arrarBool, "Массив типа Bool");
            StartShuffle(random, arrayFloat, "Массив типа Float");
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

        private static void StartShuffle<T>(Random random, T[] sourceArray, string title)
        {
            Print($"{title}\n");
            Print("Исходный массив: ");
            Print(sourceArray);

            sourceArray = Shuffle(sourceArray, random);

            Print("Перемешанный массив: ");
            Print(sourceArray);
            Print("\n");
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

        private static void Print<T>(T[] array)
        {
            foreach (T element in array)
            {
                Console.Write($"{element} ");
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