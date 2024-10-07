namespace _26_Task
{
    public class Program
    {
        static void Main()
        {
            Console.Title = "ДЗ: Сдвиг значений массива";
            Random random = new Random();
            int[] numbers;
            int minLenthNumber = 4;
            int maxLenthNumber = 20;
            int maxRandomNumber = 10;
            numbers = new int[random.Next(minLenthNumber, maxLenthNumber)];
            int userInput;
            int firstNumber;
            int shiftStepsOnLeft = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(maxRandomNumber);
            }

            Console.WriteLine($"Исходный массив, размерностью {numbers.Length} элементов:");

            foreach (int number in numbers)
            {
                Console.Write($"{number} ");
            }

            Console.Write("\n\nВведите значение на которое желаете сдвинуть элементы массива на позицию влево: ");
            userInput = Convert.ToInt32(Console.ReadLine());

            shiftStepsOnLeft = userInput % numbers.Length;

            for (int i = 0; i < shiftStepsOnLeft; i++)
            {
                firstNumber = numbers[0];

                for (int j = 0; j < numbers.Length - 1; j++)
                {
                    numbers[j] = numbers[j + 1];
                }

                numbers[numbers.Length - 1] = firstNumber;
            }

            Console.WriteLine($"Полученный массив при сдвиге влево на: {userInput}\n");

            foreach (int number in numbers)
            {
                Console.Write($"{number} ");
            }

            Console.ReadKey();
        }
    }
}