namespace _25_Task
{
    public class Program
    {
        static void Main()
        {
            Console.Title = "ДЗ: Split";
            string sourceString = "Метод Split возвращает массив строк, " +
                                  "содержащий подстроки в этом экземпляре, " +
                                  "разделенные элементами указанной строки " +
                                  "или массива символов Юникода.";

            char spaceChar = ' ';

            string[] wordsArray = sourceString.Split(spaceChar);
            Console.WriteLine("Исходный текст:\n");
            Console.WriteLine(sourceString);
            Console.WriteLine();
            Console.WriteLine("Результат метода String.Split():\n");

            foreach (string word in wordsArray)
            {
                Console.WriteLine(word);
            }

            Console.ReadKey();
        }
    }
}