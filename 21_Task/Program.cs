namespace _21_Task
{
    public class Program
    {
        private static void Main()
        {
            Console.Title = "ДЗ: Локальные максимумы";
            int[] numbers;
            int arraySize;
            int firstNumberIndex = 0;
            int lastNumberIndex;
            int maxRandomNumber = 1000;
            Random random = new Random();

            Console.Write("Введите размер массива: ");
            arraySize = Convert.ToInt32(Console.ReadLine());
            numbers = new int[arraySize];
            lastNumberIndex = arraySize - 1;

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(maxRandomNumber);
            }

            Console.WriteLine($"Одномерный массив, размерностью {arraySize} элементов:");

            foreach (int number in numbers)
            {
                Console.Write($"{number} ");
            }

            Console.WriteLine("\n\nЛокальные максимумы: ");

            if (numbers[firstNumberIndex] > numbers[firstNumberIndex + 1])
            {
                Console.Write($"{numbers[firstNumberIndex]} ");
            }

            for (int i = firstNumberIndex + 1; i < numbers.Length - 1; i++)
            {
                if (numbers[i] > numbers[i - 1] && numbers[i] > numbers[i + 1])
                {
                    Console.Write($"{numbers[i]} ");
                }
            }

            if (numbers[lastNumberIndex] > numbers[lastNumberIndex - 1])
            {
                Console.Write($"{numbers[lastNumberIndex]} ");
            }

            Console.ReadKey();
        }
    }
}