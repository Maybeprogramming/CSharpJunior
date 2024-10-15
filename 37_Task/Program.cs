namespace _37_Task
{
    public class Program
    {
        private static void Main()
        {
            Console.Title = "ДЗ: Объединение в одну коллекцию";

            List<string> collectionNumbers1 = new List<string>() { "1", "2", "1" };
            List<string> collectionNumbers2 = new List<string>() { "3", "2" };

            var mergedCollection = MergeCollections(collectionNumbers1, collectionNumbers2);

            Console.Write("Исходная коллекция №1: ");
            PrintCollection(collectionNumbers1);
            Console.Write("\nИсходная коллекция №2: ");
            PrintCollection(collectionNumbers2);
            Console.Write("\nРезультат объединения двух коллекций, ислючая повторения: ");
            PrintCollection(mergedCollection);
            Console.ReadLine();
        }

        private static List<string> MergeCollections(List<string> collectionOne, List<string> collectionTwo)
        {
            List<string> result = new List<string>();

            ExcludeIdentialNumbers(collectionOne, result);
            ExcludeIdentialNumbers(collectionTwo, result);

            return result;
        }

        private static void ExcludeIdentialNumbers(List<string> collection, List<string> result)
        {
            foreach (var item in collection)
            {
                if (result.Contains(item) == false)
                {
                    result.Add(item);
                }
            }
        }

        private static void PrintCollection(List<string> collection)
        {
            foreach (string element in collection)
            {
                Console.Write($"{element} ");
            }
        }
    }
}