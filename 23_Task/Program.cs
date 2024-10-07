using System;

namespace _23_Task
{
    public class Program
    {
        private static void Main()
        {
            Console.Title = "ДЗ: Подмассив повторений чисел";
            Random random = new Random();
            int numberRepsCount = 1;
            int tempRepsCount = 1;
            int[] numbers;
            int number;
            int arraySize;
            int maxRandomNumber = 9;

            Console.Write("Введите размер массива: ");
            arraySize = Convert.ToInt32(Console.ReadLine());
            numbers = new int[arraySize];

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(maxRandomNumber);
            }

            number = numbers[0];

            Console.WriteLine("Массив чисел: ");

            foreach (var num in numbers)
            {
                Console.Write($"{num} ");
            }

            Console.WriteLine();

            for (int i = 1; i < numbers.Length; i++)
            {
                if (numbers[i] == numbers[i - 1])
                {
                    tempRepsCount++;
                }
                else
                {
                    tempRepsCount = 1;
                }

                if (tempRepsCount > numberRepsCount)
                {
                    numberRepsCount = tempRepsCount;
                    number = numbers[i];
                }
            }

            Console.WriteLine($"Число: {number} повторяется {numberRepsCount} раз подряд");
            Console.ReadKey();
        }
    }
}