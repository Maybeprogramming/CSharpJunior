namespace _06_Task
{
    public class Program
    {
        static void Main()
        {
            Console.Title = "ДЗ: Магазин кристаллов";

            int coinsCount;
            int cristalsCount = 0;
            int priceOneCristal = 15;
            int cristalsToBuy;

            Console.WriteLine("Скольку золота у вас в кошельке?");
            coinsCount = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"Цена одного кристала равна: {priceOneCristal}");
            Console.WriteLine("Сколько кристалов желаете купить?");

            cristalsToBuy = Convert.ToInt32(Console.ReadLine());
            cristalsCount += cristalsToBuy;
            coinsCount -= cristalsToBuy * priceOneCristal;

            Console.WriteLine($"Вы купили {cristalsToBuy} кристолов!");

            Console.WriteLine(new string('-', 30));
            Console.WriteLine($"Теперь у вас в кошельке:\n" +
                              $"Золота: {coinsCount}\n" +
                              $"Кристалов: {cristalsCount}");
        }
    }
}