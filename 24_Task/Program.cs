namespace _24_Task
{
    public class Program
    {
        private static void Main()
        {
            Console.Title = "ДЗ: Сортировка чисел";
            int[] numbers;
            Random random = new Random();
            int maxNumbers = 1000;
            int maxArrayLength = 20;
            numbers = new int[maxArrayLength];

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(maxNumbers);
            }

            Console.WriteLine($"Исходный массив, размерностью {numbers.Length} элементов:");

            foreach (int number in numbers)
            {
                Console.Write($"{number} ");
            }

            for (int i = 0; i < numbers.Length - 1; i++)
            {
                int minValue = i;

                for (int j = i + 1; j < numbers.Length; j++)
                {
                    if (numbers[j] < numbers[minValue])
                    {
                        minValue = j;
                    }
                }

                int tempNumbers = numbers[minValue];
                numbers[minValue] = numbers[i];
                numbers[i] = tempNumbers;
            }

            Console.WriteLine("\n\nСортированный массив:");

            foreach (int number in numbers)
            {
                Console.Write($"{number} ");
            }

            Console.ReadKey();
        }
    }
}