
using System.Reflection.Emit;

namespace _50_Task
{
    public class Programm
    {
        public static void Main()
        {
            Console.Title = "ДЗ: Автосервис";
            CarService carService = new CarService();
            carService.Work();
        }
    }

    public class CarService
    {
        public CarService()
        {
        }

        internal void Work()
        {
            #region For Test #########################################################################################

            Cell cell = new Cell(new Detail(DetailType.Wheel), 10);
            UserUtils.Print($"{cell}");
            UserUtils.Print($"\n{cell.Detail}");

            #endregion ###############################################################################################

            Console.ReadKey();
        }
    }

    public class Detail
    {
        public Detail(DetailType detailType) => 
            DetailType = detailType;

        public DetailType DetailType { get; }

        public string Name => 
            DetailsData.GetNameByType(DetailType);

        public override string ToString() => 
            $"{Name}";
    }

    public class Cell
    {
        private Detail _detail;
        private int _amount;
        private int _maxCapacity;

        public Cell(Detail detail, int amount)
        {
            _detail = detail;
            _amount = amount;
            _maxCapacity = UserUtils.GenerateRandomNumber(0, 10);
        }

        public Detail Detail => _detail;

        public int Amount
        {
            get => _amount;
            private set => _amount = Math.Clamp(value, 0, _maxCapacity);
        }
        
        public Detail GetDetail()
        {
            Amount--;
            return new Detail(_detail.DetailType);
        }

        public override string ToString() =>
            $"<{_detail.Name}> количество: <{Amount}>";
    }

    public enum DetailType
    {
        OilFilter,
        SparkPlugs,
        AirFilter,
        Battery,
        Generator,
        TimingBelt,
        Wheel,
        BrakePads,
        Antifreeze,
        Headlight
    }

    public static class DetailsData
    {
        private static Dictionary<DetailType, string> s_detailNames = new() 
        {
            {DetailType.OilFilter,  "Масляный фильтр"},
            {DetailType.SparkPlugs, "Свечи зажигания"},
            {DetailType.AirFilter, "Воздушный фильтр"},
            {DetailType.Battery, "Аккумулятор"},
            {DetailType.Generator, "Генератор"},
            {DetailType.TimingBelt, "Ремень ГРМ"},
            {DetailType.Wheel, "Колесо"},
            {DetailType.BrakePads, "Тормозные колодки"},
            {DetailType.Antifreeze, "Антифриз"},
            {DetailType.Headlight, "Фара"}
        };

        public static string GetNameByType(DetailType type)
        {
            s_detailNames.TryGetValue(type, out string name);

            return name;
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
            ConsoleColor defaultColor = Console.ForegroundColor;
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