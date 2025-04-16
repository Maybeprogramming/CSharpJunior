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

            Cell cell = new Cell(new Detail(DetailType.Wheel, false), 10);
            UserUtils.Print($"{cell}", ConsoleColor.Green);
            UserUtils.Print($"\n{cell.Detail}");

            Detail detail = new Detail(DetailType.Wheel, false);
            Detail detail1 = new Detail(DetailType.AirFilter, true);

            Detail detail2 = new Detail(DetailType.Wheel, true);
            Detail detail3 = new Detail(DetailType.AirFilter, false);

            List<Detail> listDetails = new List<Detail>() { detail, detail1 };

            Car car = new(listDetails, "БМВ");


            car.ShowInfo();
            car.Repair(detail3);
            car.ShowInfo();

            #endregion ###############################################################################################

            Console.ReadKey();
        }
    }

    public class Warehouse
    {
        //Склад деталей
    }

    public class Detail
    {
        public Detail(DetailType detailType, bool isBroken)
        {
            DetailType = detailType;
            IsBroken = isBroken;
        }

        public DetailType DetailType { get; }

        public string Name =>
            DetailsData.GetName(DetailType);

        public bool IsBroken { get; }

        public string IsBrokenStatus =>
            IsBroken == true ? "Сломана" : "Исправна";

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
            bool isBroken = false;
            Amount--;

            return new Detail(_detail.DetailType, isBroken);
        }

        public override string ToString() =>
            $"<{_detail.Name}>, количество: <{Amount}>";
    }

    //Машина состоит из деталей
    //Количество поломанных не меньше 1 детали
    //Надо показать все детали которые поломаны -> дейтсвие которое должен сделать слесарь
    //****Сама же машина не определяет сломана она или нет, соответственно это определяет внешний класс по списку деталей которые находятся в машине
    public class Car
    {
        private List<Detail> _details;

        public Car(List<Detail> details, string model)
        {
            _details = details;
            Name = model;
        }

        public string Name { get; }

        public IEnumerable<Detail> Details => _details;

        public void Repair(Detail newDetail)
        {
            _details.Remove(GetReplaceableDetail(newDetail));
            _details.Add(newDetail);
        }

        public void ShowInfo()
        {
            int index = 0;

            UserUtils.Print($"\nИнформация об машине:" +
                            $"\nМодель <{Name}>");

            _details.ForEach((detail) => 
                UserUtils.Print($"\n{++index}. Деталь <{detail.Name}> - {detail.IsBrokenStatus}"));
        }

        private Detail GetReplaceableDetail(Detail newDetail) => 
            _details.Where(detail => detail.DetailType == newDetail.DetailType).First();
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

        public static string GetName(DetailType type)
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