namespace _16_Task
{
    public class Program
    {
        static void Main()
        {
            Console.Title = "ДЗ: Кратные числа";
            int number;
            int startRandomNumber = 10;
            int endRandomNumber = 25;
            int minNumber = 50;
            int maxlNumber = 150;
            int amountNumbers = 0;
            Random random = new Random();

            number = random.Next(startRandomNumber, ++endRandomNumber);
            Console.WriteLine($"Число N = {number}");

            for (int i = number; i <= maxlNumber; i += number)
            {
                if (i >= minNumber)
                {
                    amountNumbers++;
                }
            }

            Console.WriteLine($"Колличество кратных чисел: {amountNumbers}");
            Console.ReadLine();
        }
    }
}