namespace _30_Task
{
    internal class Program
    {
        static void Main()
        {
            Console.Title = "ДЗ: Канзас сити шафл";

            ConsoleSetup();
            Work();

            Console.ReadKey();
        }

        private static void Work()
        {
            Random random = new();

            int[] arrayNumbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
            char[] arraySymbols = { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')' };
            string[] arrayText = { "Один", "Два", "Три", "Четыре", "Пять" };
            bool[] arrarBool = { true, true, true, false, false, false };
            float[] arrayFloat = { 1.1f, 2.2f, 3.3f, 4.4f, 5.5f, 6.6f, 7.7f, 8.8f, 9.9f, 0.01f };

            arrayNumbers = Shuffle2(random, arrayNumbers);
            arraySymbols = Shuffle2(random, arraySymbols);
            arrayText = Shuffle2(random, arrayText);
            arrarBool = Shuffle2(random, arrarBool);
            arrayFloat = Shuffle2(random, arrayFloat);
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

        private static T[] Shuffle2<T>(Random random, T[] sourceArray)
        {
            Print("Исходный массив: \n");
            Print(sourceArray);

            sourceArray = Shuffle(ref sourceArray, random);

            Print("\n");
            Print("Перемешанный массив: \n");
            Print(sourceArray);

            return sourceArray;
        }

        private static T[] Shuffle<T>(ref T[] array, Random random)
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

        static void Print<T>(T[] array)
        {
            foreach (var element in array)
            {
                Console.Write($" {element}");
            }
        }

        static void Print(string text, ConsoleColor color = ConsoleColor.White)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = defaultColor;
        }
    }
}