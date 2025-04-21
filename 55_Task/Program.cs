namespace _55_Task
{
    public class Program
    {
        static void Main()
        {
            Console.Title = "ДЗ: Определение просрочки";
            Warehouse warehouse = new Warehouse();
            warehouse.Work();
        }
    }

    public class Warehouse
    {
        public void Work()
        {

        }
    }

    public class Preserves
    {
        public Preserves(string name, int productionYear, int expirationDate)
        {
            Name = name;
            ProductionDate = productionYear;
            ExpirationDate = expirationDate;
        }

        public string Name { get; }
        public int ProductionDate { get; }
        public int ExpirationDate { get; }

        public string GetInfo() =>
            $"{Name}, - Год производства: {ProductionDate}, - Срок годности до: {ExpirationDate}";
    }
    public class PreservesFactory
    {
        public List<Preserves> CreateSomePreserves(int count)
        {
            List<Preserves> preserves = new();

            for (int i = 0; i < count; i++)
            {
                preserves.Add(CreatePreserves());
            }

            return preserves;
        }

        private Preserves CreatePreserves()
        {
            string name = TakeRandomName();
            int minProductionDate = 2010;
            int maxProductionDate = 2025;
            int expirationDateRange = 10;
            int productionDate = UserUtils.GenerateRandomNumber(minProductionDate, maxProductionDate);
            int expirationDate = productionDate + expirationDateRange;

            return new Preserves(name, productionDate, expirationDate);
        }

        private string TakeRandomName()
        {
            string[] names = new string[]
            {
                "Говядина тушеная", "Свинина тушеная", "Ассорти мясное", "Каша гречневая с тушеной говядиной",
                 "Индейка тушеная", "Каша рисовая с тушеной свининой", "Холодец из свинины"
            };

            return names[UserUtils.GenerateRandomNumber(0, names.Length - 1)];
        }
    }

    public static class UserUtils
    {
        private static Random s_random = new Random();

        public static int GenerateRandomNumber(int minNumber, int maxNumber) =>
            s_random.Next(minNumber, ++maxNumber);

        public static void Print<T>(T message) =>
            Console.Write(message.ToString());

        public static void Print<T>(T message, ConsoleColor consoleColor = ConsoleColor.White)
        {
            Console.ForegroundColor = consoleColor;
            Print(message);
            Console.ResetColor();
        }

        public static int ReadInputNumber()
        {
            int result;

            while (int.TryParse(Console.ReadLine(), out result) == false)
            {
                Print($"\nВы ввели не число!\nПопробуйте снова: ", ConsoleColor.DarkYellow);
            }

            return result;
        }
    }
}