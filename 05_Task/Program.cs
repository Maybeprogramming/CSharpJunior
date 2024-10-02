namespace _05_Task
{
    internal class Program
    {
        static void Main()
        {
            Console.Title = "ДЗ: Перестановка местами значений";
            string name = "Петров";
            string lastname = "Иван";
            string bufferString;

            Console.WriteLine($"Данные до перестановки:");
            Console.WriteLine($"Имя: {name}, Фамилия: {lastname}.");

            bufferString = name;
            name = lastname;
            lastname = bufferString;

            Console.WriteLine(new string('-', 30));
            Console.WriteLine($"Данные после перестановки:");
            Console.WriteLine($"Имя: {name}, Фамилия: {lastname}.");

            Console.ReadKey();
        }
    }
}