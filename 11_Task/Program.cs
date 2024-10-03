namespace _11_Task
{
    public class Program
    {
        private static void Main()
        {
            Console.Title = "ДЗ: Сумма чисел";
            int number;
            int minNumber = 0;
            int maxNumber = 100;
            int firstNumber = 3;
            int secondNumber = 5;
            int sumNumber = 0;
            Random random = new Random();

            number = random.Next(minNumber, ++maxNumber);
            Console.WriteLine($"Числа кратные \"{firstNumber}\" и \"{secondNumber}\":");

            for (int i = 0; i <= number; i++)
            {
                if (i % firstNumber == 0 || i % secondNumber == 0)
                {
                    sumNumber += i;
                    Console.Write($" {i} ");
                }
            }

            Console.WriteLine($"\nСумма чисел: {sumNumber}");
            Console.ReadKey();
        }
    }
}