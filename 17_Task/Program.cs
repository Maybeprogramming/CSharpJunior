namespace _17_Task
{
    public class Program
    {
        static void Main()
        {
            Console.Title = "ДЗ: Степень двойки";
            int randomNumber;
            int maxRandomNumber = 1000;
            int totalNumber;
            int number = 2;
            int degreeNumber = 1;
            Random random = new Random();
            randomNumber = random.Next(maxRandomNumber);
            totalNumber = number;

            while (randomNumber >= totalNumber)
            {
                totalNumber *= number;
                degreeNumber++;
            }

            Console.WriteLine($"Число {number} в степени {degreeNumber} превосходит заданное число {randomNumber}");
            Console.WriteLine($"Результат: {totalNumber} > {randomNumber}");
            Console.ReadKey();
        }
    }
}
