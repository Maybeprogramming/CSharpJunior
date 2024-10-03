namespace _10_Task
{
    public class Program
    {
        private static void Main()
        {
            Console.Title = "ДЗ: Последовательность";
            int startNumber = 5;
            int endNumber = 103;
            int stepNumber = 7;

            for (int i = startNumber; i <= endNumber; i += stepNumber)
            {
                Console.Write(i + " ");
            }

            Console.ReadKey();
        }
    }
}

//Ответ: данный цикл For выбрал потому что известно начало, конец и шаг в цикле