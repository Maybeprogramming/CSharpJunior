namespace _20_Task
{
    public class Program
    {
        static void Main()
        {
            Console.Title = "ДЗ: Наибольший элемент";
            Random random = new Random();
            int rowsCount = 10;
            int collumsCount = 10;
            int[,] numbers = new int[rowsCount, collumsCount];
            int minRandomNumber = -99;
            int maxRandomNumber = 99;
            int zeroValue = 0;
            int maxNumber = int.MinValue;

            Console.WriteLine($"Исходная матрица, размерностью: {rowsCount}х{collumsCount}\n");

            for (int i = 0; i < numbers.GetLength(0); i++)
            {
                for (int j = 0; j < numbers.GetLength(1); j++)
                {
                    numbers[i, j] = random.Next(minRandomNumber, maxRandomNumber);
                    Console.Write($" {numbers[i, j]} \t");

                    if (numbers[i, j] > maxNumber)
                    {
                        maxNumber = numbers[i, j];
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine("\nПолученная матрица:\n");

            for (int i = 0; i < numbers.GetLength(0); i++)
            {
                for (int j = 0; j < numbers.GetLength(1); j++)
                {
                    if (numbers[i, j] == maxNumber)
                    {
                        numbers[i, j] = zeroValue;
                    }

                    Console.Write($" {numbers[i, j]} \t");
                }

                Console.WriteLine();
            }

            Console.WriteLine($"\nМаксимальное число в матрице равно: {maxNumber}");
            Console.ReadKey();
        }
    }
}
