namespace _03_Task
{
    internal class Program
    {
        static void Main()
        {
            Console.Title = "ДЗ: Работа со строками";
            string name;
            string zodiacSign;
            string age;
            string workPlace;

            Console.WriteLine("Как ваше имя?");
            name = Console.ReadLine();

            Console.WriteLine("Сколько вам лет?");
            age = Console.ReadLine();

            Console.WriteLine("Какой ваш знак задиака?");
            zodiacSign = Console.ReadLine();

            Console.WriteLine("Где вы работаете?");
            workPlace = Console.ReadLine();

            Console.WriteLine(new string('-', 20));
            Console.WriteLine($"Вас зовут {name}, вам {age}, вы {zodiacSign} и работаете на {workPlace}.");

            Console.ReadKey();
        }
    }
}
