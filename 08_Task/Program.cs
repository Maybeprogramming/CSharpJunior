namespace _08_Task
{
    public class Program
    {
        static void Main()
        {
            Console.Title = "ДЗ: Освоение циклов";
            string userText;
            int amountPrint;

            Console.WriteLine("Введите сообщение:");
            userText = Console.ReadLine();

            Console.Write("Сколько раз хотите вывести? ");
            amountPrint = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < amountPrint; i++)
            {
                Console.WriteLine(userText);
            }

            Console.ReadKey();
        }
    }
}